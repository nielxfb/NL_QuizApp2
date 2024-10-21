using QuizApp.Domain.Entities;

namespace QuizApp.Domain.Interfaces;

public interface IQuestionRepository
{
    Task AddAsync(Question question);
    Task RemoveAsync(Question question);
    Task UpdateAsync(Question question);
    Task<List<Question>> GetByQuizIdAsync(Guid quizId);
    Task<Question?> GetByIdAsync(Guid questionId);
}