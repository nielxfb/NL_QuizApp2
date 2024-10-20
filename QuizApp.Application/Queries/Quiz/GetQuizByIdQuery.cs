namespace QuizApp.Application.Queries.Quiz;

public class GetQuizByIdQuery
{
    public Guid Id { get; set; }

    public GetQuizByIdQuery(Guid id)
    {
        Id = id;
    }
}