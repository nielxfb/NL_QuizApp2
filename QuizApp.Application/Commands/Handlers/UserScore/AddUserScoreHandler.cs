using QuizApp.Application.Commands.UserScore;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Commands.Handlers.UserScore;

public class AddUserScoreHandler : ICommandHandler<AddUserScoreCommand>
{
    private readonly IUserScoreRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly IScheduleRepository _scheduleRepository;

    public AddUserScoreHandler(IUserScoreRepository repository, IUserRepository userRepository, IScheduleRepository scheduleRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
        _scheduleRepository = scheduleRepository;
    }

    public async Task HandleAsync(AddUserScoreCommand command)
    {
        var user = await _userRepository.GetByIdAsync(command.UserId);
        if (user == null)
            throw new ArgumentException("User not found.");

        var schedule = await _scheduleRepository.GetByIdAsync(command.ScheduleId);
        if (schedule == null)
            throw new ArgumentException("Schedule not found.");

        var correctAnswers = 0;
        foreach (var option in command.SelectedOptions)
        {
            if (option.IsCorrect)
                correctAnswers++;
        }

        var existingScore = await _repository.GetAsync(command.UserId, command.ScheduleId);
        if (existingScore != null)
        {
            existingScore.Score = (float) correctAnswers / command.QuestionCount;
            await _repository.UpdateAsync(existingScore);
            return;
        }
        
        var userScore = new Domain.Entities.UserScore
        {
            UserId = command.UserId,
            ScheduleId = command.ScheduleId,
            Score = (float) correctAnswers / command.QuestionCount,
        };

        await _repository.AddAsync(userScore);
    }
}