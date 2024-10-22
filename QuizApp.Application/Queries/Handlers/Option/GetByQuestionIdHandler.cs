using QuizApp.Application.DTOs.Option;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Application.Queries.Option;
using QuizApp.Application.Queries.Schedule;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Queries.Handlers.Option;

public class GetByQuestionIdHandler : IQueryHandler<GetByQuestionIdQuery, List<OptionDto>>
{
    private readonly IOptionRepository _repository;
    private readonly IQuestionRepository _questionRepository;

    public GetByQuestionIdHandler(IOptionRepository repository, IQuestionRepository questionRepository)
    {
        _repository = repository;
        _questionRepository = questionRepository;
    }

    public async Task<List<OptionDto>> HandleAsync(GetByQuestionIdQuery query)
    {
        var question = await _questionRepository.GetByIdAsync(query.QuestionId);
        if (question == null) throw new ArgumentException("Question not found.");

        var options = await _repository.GetByQuestionIdAsync(query.QuestionId);
        return options.Select(o => new OptionDto
        {
            QuestionId = o.QuestionId,
            OptionChoice = o.OptionChoice,
            OptionText = o.OptionText,
            ImageUrl = o.ImageUrl,
            IsCorrect = o.IsCorrect
        }).ToList();
    }
}