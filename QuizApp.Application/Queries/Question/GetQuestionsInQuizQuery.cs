using QuizApp.Application.DTOs.Question;

namespace QuizApp.Application.Queries.Question;

public class GetQuestionsInQuizQuery
{
    public Guid QuizId { get; set; }

    public GetQuestionsInQuizQuery(Guid quizId)
    {
        QuizId = quizId;
    }
}