using QuizApp.Application.DTOs.Option;
using QuizApp.Application.DTOs.Question;
using QuizApp.Application.DTOs.Response;
using QuizApp.Application.Interfaces.Handlers;
using QuizApp.Application.Queries.Response;
using QuizApp.Domain.Interfaces;

namespace QuizApp.Application.Queries.Handlers.Response;

public class GetUserResponsesInQuizHandler : IQueryHandler<GetUserResponsesInQuizQuery, ResponseDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IQuizRepository _quizRepository;
    private readonly IResponseRepository _repository;

    public GetUserResponsesInQuizHandler(IUserRepository userRepository, IQuizRepository quizRepository, IResponseRepository repository)
    {
        _userRepository = userRepository;
        _quizRepository = quizRepository;
        _repository = repository;
    }


    public async Task<ResponseDto> HandleAsync(GetUserResponsesInQuizQuery query)
    {
        var user = await _userRepository.GetByIdAsync(query.UserId);
        if (user == null)
        {
            throw new ArgumentException("User not found");
        }
        
        var quiz = await _quizRepository.GetByIdAsync(query.QuizId);
        if (quiz == null)
        {
            throw new ArgumentException("Quiz not found");
        }
        
        var responses = await _repository.GetUserResponsesInQuizAsync(query.UserId, query.QuizId);
        var score = (float) responses.Where(r => r.Option.IsCorrect).ToList().Count / quiz.Questions.Count;
        return new ResponseDto
        {
            Quiz = quiz,
            SelectedOptions = responses.Select(r => new SelectedOptionDto
            {
                Question = new QuestionDto
                {
                    QuestionId = r.QuestionId,
                    QuestionText = r.Question.QuestionText,
                    ImageUrl = r.Question.ImageUrl,
                },
                SelectedOption = new OptionDto
                {
                    QuestionId = r.QuestionId,
                    ImageUrl = r.Option.ImageUrl,
                    IsCorrect = r.Option.IsCorrect,
                    OptionText = r.Option.OptionText,
                },
                IsCorrect = r.IsCorrect,

            }).ToList(),
            Score = score,
        };
    }
}