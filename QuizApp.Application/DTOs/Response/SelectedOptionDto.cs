using QuizApp.Application.DTOs.Option;
using QuizApp.Application.DTOs.Question;

namespace QuizApp.Application.DTOs.Response;

public class SelectedOptionDto
{
    public QuestionDto Question { get; set; }
    public OptionDto SelectedOption { get; set; }
    public bool IsCorrect { get; set; }
    public DateTime AnsweredAt { get; set; }
}