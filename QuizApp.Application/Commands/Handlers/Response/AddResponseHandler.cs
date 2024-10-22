using QuizApp.Application.Commands.Response;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Commands.Handlers.Response;

public class AddResponseHandler : ICommandHandler<AddResponseCommand>
{
    private readonly IResponseRepository _repository;
    private readonly IQuizRepository _quizRepository;
    private readonly IUserRepository _userRepository;
    private readonly IOptionRepository _optionRepository;

    public AddResponseHandler(IResponseRepository repository, IQuizRepository quizRepository,
        IUserRepository userRepository, IOptionRepository optionRepository)
    {
        _repository = repository;
        _quizRepository = quizRepository;
        _userRepository = userRepository;
        _optionRepository = optionRepository;
    }

    public async Task HandleAsync(AddResponseCommand command)
    {
        var quiz = await _quizRepository.GetByIdAsync(command.QuizId);
        if (quiz == null) throw new ArgumentException("Quiz not found.");

        var user = await _userRepository.GetByIdAsync(command.UserId);
        if (user == null) throw new ArgumentException("User not found.");

        var option = await _optionRepository.GetByIdAsync(command.QuestionId, command.OptionChoice);
        if (option == null) throw new ArgumentException("Option not found.");

        var isCorrect = option.IsCorrect;

        var response = new Domain.Entities.Response
        {
            ResponseId = Guid.NewGuid(),
            QuizId = command.QuizId,
            UserId = command.UserId,
            QuestionId = command.QuestionId,
            OptionChoice = command.OptionChoice,
            IsCorrect = isCorrect,
            AnsweredAt = command.AnsweredAt
        };

        await _repository.AddAsync(response);
    }
}