using QuizApp.Application.DTOs.Question;

namespace QuizApp.Application.DTOs.Response;

public class ResponseDto
{
    public Domain.Entities.Quiz Quiz { get; set; }
    public List<SelectedOptionDto> SelectedOptions { get; set; }
    public float Score { get; set; }
}