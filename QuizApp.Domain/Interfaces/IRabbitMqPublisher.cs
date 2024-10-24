namespace QuizApp.Domain.Interfaces;

public interface IRabbitMqPublisher<in T>
{
    Task PublishMessageAsync(T message);
}