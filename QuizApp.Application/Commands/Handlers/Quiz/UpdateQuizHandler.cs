using QuizApp.Application.Commands.Quiz;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Commands.Handlers.Quiz;

public class UpdateQuizHandler : ICommandHandler<UpdateQuizCommand>
{
    private readonly IQuizRepository _repository;

    public UpdateQuizHandler(IQuizRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(UpdateQuizCommand command)
    {
        var quiz = await _repository.GetByIdAsync(new QuizId(command.Id));
        if (quiz == null)
        {
            throw new ArgumentException("Quiz not found.");
        }

        quiz.Title = command.Title;
        await _repository.UpdateAsync(quiz);
    }
}