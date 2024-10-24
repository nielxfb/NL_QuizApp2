using QuizApp.Application.Commands.UserScore;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Commands.Handlers.UserScore;

public class AddUserScoreHandler : ICommandHandler<AddUserScoreCommand>
{
    private readonly IUserScoreRepository _repository;
    private readonly IUserRepository _userRepository;
    private readonly IQuizRepository _quizRepository;

    public AddUserScoreHandler(IUserScoreRepository repository, IUserRepository userRepository, IQuizRepository quizRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
        _quizRepository = quizRepository;
    }

    public async Task HandleAsync(AddUserScoreCommand command)
    {
        var user = await _userRepository.GetByIdAsync(command.UserId);
        if (user == null)
            throw new ArgumentException("User not found.");

        var quiz = await _quizRepository.GetByIdAsync(command.QuizId);
        if (quiz == null)
            throw new ArgumentException("Quiz not found.");

        var correctAnswers = 0;
        foreach (var option in command.SelectedOptions)
        {
            if (option.IsCorrect)
                correctAnswers++;
        }
        
        var userScore = new Domain.Entities.UserScore
        {
            UserId = command.UserId,
            QuizId = command.QuizId,
            Score = (float) correctAnswers / command.QuestionCount,
        };

        await _repository.AddAsync(userScore);
    }
}