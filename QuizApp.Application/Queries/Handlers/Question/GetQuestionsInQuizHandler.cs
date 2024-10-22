using QuizApp.Application.DTOs.Option;
using QuizApp.Application.DTOs.Question;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Application.Queries.Question;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Queries.Handlers.Question;

public class GetQuestionsInQuizHandler : IQueryHandler<GetQuestionsInQuizQuery, List<QuestionDto>>
{
    private readonly IQuestionRepository _repository;
    private readonly IQuizRepository _quizRepository;

    public GetQuestionsInQuizHandler(IQuestionRepository repository, IQuizRepository quizRepository)
    {
        _repository = repository;
        _quizRepository = quizRepository;
    }

    public async Task<List<QuestionDto>> HandleAsync(GetQuestionsInQuizQuery query)
    {
        var quiz = await _quizRepository.GetByIdAsync(query.QuizId);
        if (quiz == null) throw new ArgumentException("Quiz not found.");

        var questions = await _repository.GetByQuizIdAsync(query.QuizId);
        return questions.Select(q => new QuestionDto
        {
            QuestionId = q.QuestionId,
            QuestionText = q.QuestionText,
            ImageUrl = q.ImageUrl,
            Options = q.Options.Select(o => new OptionDto
            {
                QuestionId = o.QuestionId,
                OptionChoice = o.OptionChoice,
                OptionText = o.OptionText,
                IsCorrect = o.IsCorrect,
                ImageUrl = o.ImageUrl,
            }).ToList()
        }).ToList();
    }
}