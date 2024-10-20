using QuizApp.Application.DTOs.Quiz;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Application.Queries.Quiz;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Queries.Handlers.Quiz;

public class GetQuizByIdHandler : IQueryHandler<GetQuizByIdQuery, QuizDetailsDto>
{
    private readonly IQuizRepository _repository;

    public GetQuizByIdHandler(IQuizRepository repository)
    {
        _repository = repository;
    }

    public async Task<QuizDetailsDto> HandleAsync(GetQuizByIdQuery query)
    {
        var quiz = await _repository.GetByIdAsync(new QuizId(query.Id));
        if (quiz == null)
        {
            throw new ArgumentException("Quiz not found.");
        }
        
        return new QuizDetailsDto
        {
            Id = quiz.Id.Value,
            Title = quiz.Title,
        };
    }
}