using QuizApp.Application.Commands.Response;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Commands.Handlers.Response;

public class UpdateResponseHandler : ICommandHandler<UpdateResponseCommand>
{
    private readonly IResponseRepository _repository;

    public UpdateResponseHandler(IResponseRepository repository)
    {
        _repository = repository;
    }

    public async Task HandleAsync(UpdateResponseCommand command)
    {
        var response = await _repository.GetByIdAsync(command.ResponseId);
        if (response == null)
        {
            throw new ArgumentException("Response not found.");
        }

        response.OptionChoice = command.OptionChoice;
        response.AnsweredAt = command.AnsweredAt;

        await _repository.UpdateAsync(response);
    }
}