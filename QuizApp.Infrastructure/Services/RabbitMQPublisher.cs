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

    public RabbitMqPublisher(IOptions<RabbitMqConfig> rabbitMqConfig)
    {
        _rabbitMqConfig = rabbitMqConfig.Value;
    }

    public async Task PublishMessageAsync(T message, string queueName)
    {
        var factory = new ConnectionFactory
        {
            HostName = _rabbitMqConfig.HostName,
            UserName = _rabbitMqConfig.UserName,
            Password = _rabbitMqConfig.Password,
        };

        using (var connection = factory.CreateConnection())
        {
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false,
                    arguments: null);

                var jsonMessage = JsonSerializer.Serialize(message);
                var body = Encoding.UTF8.GetBytes(jsonMessage);


                await Task.Run(() =>
                    channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body));
            }
        }
    }
}