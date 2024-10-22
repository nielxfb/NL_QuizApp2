using QuizApp.Application.Commands.Question;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Commands.Handlers.Question;

public class UpdateQuestionHandler : ICommandHandler<UpdateQuestionCommand>
{
    private readonly IQuestionRepository _repository;
    private readonly IQuizRepository _quizRepository;

    public UpdateQuestionHandler(IQuestionRepository repository, IQuizRepository quizRepository)
    {
        _repository = repository;
        _quizRepository = quizRepository;
    }


    public async Task HandleAsync(UpdateQuestionCommand command)
    {
        var quiz = await _quizRepository.GetByIdAsync(command.QuizId);
        if (quiz == null) throw new ArgumentException("Quiz not found.");

        var question = await _repository.GetByIdAsync(command.QuestionId);
        if (question == null) throw new ArgumentException("Question not found.");

        question.QuizId = command.QuizId;
        question.QuestionText = command.QuestionText;
        question.ImageUrl = command.ImageUrl;

        await _repository.UpdateAsync(question);
    }
}