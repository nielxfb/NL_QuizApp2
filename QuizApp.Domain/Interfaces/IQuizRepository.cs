using QuizApp.Domain.Entities;

namespace QuizApp.Domain.Interfaces;

public interface IQuizRepository
{
    Task AddAsync(Quiz quiz);
    Task<ICollection<Quiz>> GetAllAsync();
    Task<Quiz?> GetByIdAsync(Guid quizId);
    Task UpdateAsync(Quiz quiz);
    Task RemoveAsync(Quiz quiz);
}