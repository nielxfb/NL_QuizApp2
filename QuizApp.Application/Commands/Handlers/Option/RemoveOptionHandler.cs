using QuizApp.Application.Commands.Option;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Commands.Handlers.Option;

public class RemoveOptionHandler : ICommandHandler<RemoveOptionCommand>
{
    private readonly IOptionRepository _repository;

    public RemoveOptionHandler(IOptionRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(RemoveOptionCommand command)
    {
        var option = await _repository.GetByIdAsync(command.QuestionId, command.OptionChoice);
        if (option == null)
        {
            throw new ArgumentException("Option not found.");
        }
        
        await _repository.RemoveAsync(option);
    }
}