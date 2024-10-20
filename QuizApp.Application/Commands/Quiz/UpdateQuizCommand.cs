using QuizApp.Application.DTOs;
using QuizApp.Application.DTOs.Quiz;

namespace QuizApp.Application.Commands.Quiz;

public class UpdateQuizCommand
{
    public Guid Id { get; set; }
    public string Title { get; set; }

    public UpdateQuizCommand(UpdateQuizDto dto)
    {
        Id = dto.Id;
        Title = dto.Title;
    }
}