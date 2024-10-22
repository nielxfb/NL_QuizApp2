using QuizApp.Application.DTOs;
using QuizApp.Application.DTOs.Quiz;
using QuizApp.Application.DTOs.User;

namespace QuizApp.Application.Commands.Quiz;

public class AddQuizCommand
{
    public string Title { get; }

    public AddQuizCommand(AddQuizDto dto)
    {
        Title = dto.Title;
    }
}