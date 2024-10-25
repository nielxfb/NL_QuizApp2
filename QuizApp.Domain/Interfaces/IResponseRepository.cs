using QuizApp.Domain.Entities;

namespace QuizApp.Domain.Interfaces;

public interface IResponseRepository
{
    Task AddAsync(Response response);
    Task UpdateAsync(Response response);
    Task<List<Response>> GetUserResponsesInQuizAsync(Guid userId, Guid scheduleId);
    Task<Response?> GetByIdAsync(Guid responseId);
    Task<Response?> GetExistingResponse(Guid userId, Guid questionId, Guid scheduleId);
}