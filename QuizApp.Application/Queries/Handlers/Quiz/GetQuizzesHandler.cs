using QuizApp.Application.DTOs.Quiz;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Application.Queries.Quiz;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Queries.Handlers.Quiz;

public class GetQuizzesHandler : IQueryHandler<GetQuizzesQuery, List<QuizDetailsDto>>
{
    private readonly IQuizRepository _repository;

    public GetQuizzesHandler(IQuizRepository repository)
    {
        _repository = repository;
    }

    public async Task<List<QuizDetailsDto>> HandleAsync(GetQuizzesQuery query)
    {
        var quizzes = await _repository.GetAllAsync();

        return quizzes.Select(quiz => new QuizDetailsDto
        {
            Id = quiz.QuizId,
            Title = quiz.Title
        }).ToList();
    }
}