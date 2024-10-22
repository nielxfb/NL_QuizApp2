using QuizApp.Application.Commands.Question;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Commands.Handlers.Question;

public class AddQuestionHandler : ICommandHandler<AddQuestionCommand>
{
    private readonly IQuestionRepository _repository;
    private readonly IQuizRepository _quizRepository;

    public AddQuestionHandler(IQuestionRepository repository, IQuizRepository quizRepository)
    {
        _repository = repository;
        _quizRepository = quizRepository;
    }

    public async Task HandleAsync(AddQuestionCommand command)
    {
        var quiz = await _quizRepository.GetByIdAsync(command.QuizId);
        if (quiz == null) throw new ArgumentException("Quiz not found.");

        var question = new Domain.Entities.Question
        {
            QuestionId = Guid.NewGuid(),
            QuizId = command.QuizId,
            QuestionText = command.QuestionText,
            ImageUrl = command.ImageUrl
        };

        await _repository.AddAsync(question);
    }
}