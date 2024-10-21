namespace QuizApp.Application.DTOs.Option;

public class RemoveOptionDto
{
    public Guid QuestionId { get; set; }
    public string OptionChoice { get; set; }
}