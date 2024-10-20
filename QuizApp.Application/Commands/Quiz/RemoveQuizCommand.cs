namespace QuizApp.Application.Commands.Quiz;

public class RemoveQuizCommand
{
    public Guid Id { get; set; }

    public RemoveQuizCommand(Guid id)
    {
        Id = id;
    }
}