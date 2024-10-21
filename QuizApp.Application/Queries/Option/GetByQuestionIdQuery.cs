namespace QuizApp.Application.Queries.Option;

public class GetByQuestionIdQuery
{
    public Guid QuestionId { get; set; }

    public GetByQuestionIdQuery(Guid questionId)
    {
        QuestionId = questionId;
    }
}