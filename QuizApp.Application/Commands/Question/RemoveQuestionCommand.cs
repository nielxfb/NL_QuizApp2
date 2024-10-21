using QuizApp.Application.DTOs.Question;

namespace QuizApp.Application.Commands.Question;

public class RemoveQuestionCommand
{
    public Guid QuestionId { get; set; }

    public RemoveQuestionCommand(Guid questionId)
    {
        QuestionId = questionId;
    }
}