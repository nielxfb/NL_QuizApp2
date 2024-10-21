using QuizApp.Domain.Entities;

namespace QuizApp.Domain.Interfaces;

public interface IResponseRepository
{
    Task AddAsync(Response response);
    Task UpdateAsync(Response response);
    Task RemoveAsync(Response response);
    Task<List<Response>> GetUserResponsesInQuizAsync(Guid userId, Guid quizId);
}