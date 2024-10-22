using QuizApp.Application.Commands.Question;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Commands.Handlers.Question;

public class AddImageUrlHandler : ICommandHandler<AddImageUrlCommand>
{
    private readonly IQuestionRepository _repository;

    public AddImageUrlHandler(IQuestionRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(AddImageUrlCommand command)
    {
        var question = await _repository.GetByIdAsync(command.QuestionId);
        if (question == null)
        {
            throw new ArgumentException("Question not found.");
        }
        
        question.ImageUrl = command.ImageUrl;
        await _repository.UpdateAsync(question);
    }
}