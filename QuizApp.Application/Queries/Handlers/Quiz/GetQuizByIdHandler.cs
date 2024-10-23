using QuizApp.Application.DTOs.Option;
using QuizApp.Application.DTOs.Question;
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
        var quiz = await _repository.GetByIdAsync(query.Id);
        if (quiz == null) throw new ArgumentException("Quiz not found.");

        return new QuizDetailsDto
        {
            Id = quiz.QuizId,
            Title = quiz.Title,
            Questions = quiz.Questions.Select(q => new QuestionDto
            {
                QuestionId = q.QuestionId,
                QuestionText = q.QuestionText,
                ImageUrl = q.ImageUrl,
                Options = q.Options.Select(o => new OptionDto
                {
                    OptionText = o.OptionText,
                    IsCorrect = o.IsCorrect,
                    OptionChoice = o.OptionChoice,
                    QuestionId = o.QuestionId,
                }).ToList()
            }).ToList()
        };
    }
}