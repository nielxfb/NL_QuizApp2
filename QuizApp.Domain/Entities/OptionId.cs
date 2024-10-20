namespace QuizApp.Domain.Entities;

public class OptionId
{
    public QuestionId QuestionId { get; }
    public char OptionChoice { get; }
    
    public OptionId(QuestionId questionId, char optionChoice)
    {
        QuestionId = questionId;
        OptionChoice = optionChoice;
    }
}