using QuizApp.Application.Commands.Option;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Commands.Handlers.Option;

public class AddOptionHandler: ICommandHandler<AddOptionCommand>
{
    private readonly IOptionRepository _repository;
    private readonly IQuestionRepository _questionRepository;

    public AddOptionHandler(IOptionRepository repository, IQuestionRepository questionRepository)
    {
        _repository = repository;
        _questionRepository = questionRepository;
    }

    public async Task HandleAsync(AddOptionCommand command)
    {
        var question = await _questionRepository.GetByIdAsync(command.QuestionId);
        if (question == null)
        {
            throw new ArgumentException("Question not found.");
        }
        
        var option = new Domain.Entities.Option
        {
            QuestionId = command.QuestionId,
            OptionChoice = command.OptionChoice,
            OptionText = command.OptionText,
            ImageUrl = command.ImageUrl,
            IsCorrect = command.IsCorrect,
        };

        await _repository.AddAsync(option);
    }
}