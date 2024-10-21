using QuizApp.Application.Commands.Question;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Commands.Handlers.Question;

public class RemoveQuestionHandler : ICommandHandler<RemoveQuestionCommand>
{
    private readonly IQuestionRepository _repository;

    public RemoveQuestionHandler(IQuestionRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(RemoveQuestionCommand command)
    {
        var question = await _repository.GetByIdAsync(command.QuestionId);
        if (question == null)
        {
            throw new ArgumentException("Question not found.");
        }
        
        await _repository.RemoveAsync(question);
    }
}