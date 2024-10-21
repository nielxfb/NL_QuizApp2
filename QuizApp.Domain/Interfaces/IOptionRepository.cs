using QuizApp.Domain.Entities;

namespace QuizApp.Domain.Interfaces;

public interface IOptionRepository
{
    Task AddAsync(Option option);
    Task UpdateAsync(Option option);
    Task RemoveAsync(Option option);
    Task<List<Option>> GetByQuestionIdAsync(Guid questionId);
    Task<Option?> GetByIdAsync(Guid questionId, char optionChoice);
}