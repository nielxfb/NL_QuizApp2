using System.Text;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using QuizApp.Application.Commands.Response;
using QuizApp.Application.DTOs.Response;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Infrastructure.Configurations;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace QuizApp.Application.Services;

public class ResponsesConsumerService : BackgroundService
{
    private readonly RabbitMqConfig _rabbitMqConfig;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<ResponsesConsumerService> _logger;
    private readonly IConnection _connection;
    private readonly IModel _channel;

    public ResponsesConsumerService(IOptions<RabbitMqConfig> rabbitMqConfig, ILogger<ResponsesConsumerService> logger, IServiceProvider serviceProvider)
    {
        _rabbitMqConfig = rabbitMqConfig.Value;
        _serviceProvider = serviceProvider;
        _logger = logger;

        var factory = new ConnectionFactory
        {
            HostName = _rabbitMqConfig.HostName,
            UserName = _rabbitMqConfig.UserName,
            Password = _rabbitMqConfig.Password,
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(queue: "responses-queue", durable: false, exclusive: false, autoDelete: false,
            arguments: null);
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        stoppingToken.ThrowIfCancellationRequested();

        var consumer = new EventingBasicConsumer(_channel);

        consumer.Received += async (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);

            var processedSuccessfully = false;
            try
            {
                processedSuccessfully = await HandleMessageAsync(message, stoppingToken);
            }
            catch (Exception e)
            {
                _logger.LogError($"An error occured when processing responses-queue:\n{e.Message}");
            }

            if (processedSuccessfully)
                _channel.BasicAck(ea.DeliveryTag, false);
            else
                _channel.BasicReject(ea.DeliveryTag, false);
        };

        _channel.BasicConsume(queue: "responses-queue", autoAck: false, consumer: consumer);
        await Task.CompletedTask;
    }

    private async Task<bool> HandleMessageAsync(string message, CancellationToken stoppingToken)
    {
        try
        {
            using var scope = _serviceProvider.CreateScope();
            var addResponseHandler =
                scope.ServiceProvider.GetRequiredService<ICommandHandler<AddResponseCommand>>();
            var addResponseDto = JsonSerializer.Deserialize<AddResponseDto>(message);
            var addResponseCommand = new AddResponseCommand(addResponseDto!);
            await addResponseHandler.HandleAsync(addResponseCommand);

            return true;
        }
        catch (Exception e)
        {
            _logger.LogError($"An error occured when processing responses-queue:\n{e.Message}");
            return false;
        }
    }

    public override void Dispose()
    {
        _channel.Close();
        _connection.Close();
        base.Dispose();
    }
}