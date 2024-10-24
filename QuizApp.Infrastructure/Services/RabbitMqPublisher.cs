using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using QuizApp.Domain.Interfaces;
using QuizApp.Infrastructure.Configurations;
using RabbitMQ.Client;

namespace QuizApp.Infrastructure.Services;

public class RabbitMqPublisher<T> : IRabbitMqPublisher<T>
{
    private readonly RabbitMqConfig _rabbitMqConfig;
    private readonly IModel _channel;

    public RabbitMqPublisher(IOptions<RabbitMqConfig> rabbitMqConfig)
    {
        _rabbitMqConfig = rabbitMqConfig.Value;
        var factory = new ConnectionFactory
        {
            HostName = _rabbitMqConfig.HostName,
            UserName = _rabbitMqConfig.UserName,
            Password = _rabbitMqConfig.Password,
        };

        var connection = factory.CreateConnection();
        _channel = connection.CreateModel();

        _channel.QueueDeclare(queue: "responses-queue", durable: false, exclusive: false, autoDelete: false,
            arguments: null);
    }

    public async Task PublishMessageAsync(T message)
    {
        var jsonMessage = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonMessage);


        await Task.Run(() =>
            _channel.BasicPublish(exchange: "", routingKey: "responses-queue", basicProperties: null, body: body));
    }
}