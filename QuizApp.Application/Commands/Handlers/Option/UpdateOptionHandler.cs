using QuizApp.Application.Commands.Option;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Commands.Handlers.Option;

public class UpdateOptionHandler : ICommandHandler<UpdateOptionCommand>
{
    private readonly IOptionRepository _repository;
    private readonly IQuestionRepository _questionRepository;

    public UpdateOptionHandler(IOptionRepository repository, IQuestionRepository questionRepository)
    {
        _repository = repository;
        _questionRepository = questionRepository;
    }

    public async Task HandleAsync(UpdateOptionCommand command)
    {
        var question = await _questionRepository.GetByIdAsync(command.QuestionId);
        if (question == null)
        {
            throw new ArgumentException("Question not found.");
        }

        var option = await _repository.GetByIdAsync(command.QuestionId, command.OptionChoice);
        if (option == null)
        {
            throw new ArgumentException("Option not found.");
        }

        option.OptionText = command.OptionText;
        option.ImageUrl = command.ImageUrl;
        option.IsCorrect = command.IsCorrect;

        await _repository.UpdateAsync(option);
    }
}