using QuizApp.Application.Commands.Quiz;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Commands.Handlers.Quiz;

public class RemoveQuizHandler : ICommandHandler<RemoveQuizCommand>
{
    private readonly IQuizRepository _repository;
    
    public RemoveQuizHandler(IQuizRepository repository)
    {
        _repository = repository;
    }


    public async Task HandleAsync(RemoveQuizCommand command)
    {
        var quiz = await _repository.GetByIdAsync(new QuizId(command.Id));
        if (quiz == null)
        {
            throw new ArgumentException("Quiz not found.");
        }
        
        await _repository.RemoveAsync(quiz);
    }
}