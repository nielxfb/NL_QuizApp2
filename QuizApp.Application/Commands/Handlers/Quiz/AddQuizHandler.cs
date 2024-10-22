using QuizApp.Application.Commands.Quiz;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Commands.Handlers.Quiz;

public class AddQuizHandler : ICommandHandler<AddQuizCommand>
{
    private readonly IQuizRepository _repository;

    public AddQuizHandler(IQuizRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(AddQuizCommand command)
    {
        var quiz = new Domain.Entities.Quiz()
        {
            QuizId = Guid.NewGuid(),
            Title = command.Title
        };

        await _repository.AddAsync(quiz);
    }
}