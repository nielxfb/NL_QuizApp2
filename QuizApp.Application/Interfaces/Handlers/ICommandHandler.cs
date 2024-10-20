namespace QuizApp.Application.Interfaces.Handlers;

public interface ICommandHandler<TCommand>
{
    Task HandleAsync(TCommand command);
}