using QuizApp.Application.Commands.Response;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Commands.Handlers.Response;

public class AddResponseHandler : ICommandHandler<AddResponseCommand>
{
    private readonly IResponseRepository _repository;
    private readonly IScheduleRepository _scheduleRepository;
    private readonly IUserRepository _userRepository;
    private readonly IOptionRepository _optionRepository;

    public AddResponseHandler(IResponseRepository repository, IScheduleRepository scheduleRepository, IUserRepository userRepository, IOptionRepository optionRepository)
    {
        _repository = repository;
        _scheduleRepository = scheduleRepository;
        _userRepository = userRepository;
        _optionRepository = optionRepository;
    }

    public async Task HandleAsync(AddResponseCommand command)
    {
        var schedule = await _scheduleRepository.GetByIdAsync(command.ScheduleId);
        if (schedule == null) throw new ArgumentException("Schedule not found.");

        var user = await _userRepository.GetByIdAsync(command.UserId);
        if (user == null) throw new ArgumentException("User not found.");

        var option = await _optionRepository.GetByIdAsync(command.QuestionId, command.OptionChoice);
        if (option == null) throw new ArgumentException("Option not found.");

        var isCorrect = option.IsCorrect;
            
        var existingResponse = await _repository.GetExistingResponse(command.UserId, command.QuestionId);
        if (existingResponse == null)
        {
            var response = new Domain.Entities.Response
            {
                ResponseId = Guid.NewGuid(),
                ScheduleId = command.ScheduleId,
                UserId = command.UserId,
                QuestionId = command.QuestionId,
                OptionChoice = command.OptionChoice,
                IsCorrect = isCorrect,
                AnsweredAt = command.AnsweredAt
            };

            await _repository.AddAsync(response);
        }
        else
        {
            existingResponse.OptionChoice = command.OptionChoice;
            existingResponse.AnsweredAt = command.AnsweredAt;
            existingResponse.IsCorrect = isCorrect;

            await _repository.UpdateAsync(existingResponse);
        }
    }
}