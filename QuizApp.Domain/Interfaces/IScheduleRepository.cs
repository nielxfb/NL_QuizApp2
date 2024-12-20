using QuizApp.Domain.Entities;

namespace QuizApp.Domain.Interfaces;

public interface IScheduleRepository
{
    Task AddAsync(Schedule schedule);
    Task<ICollection<Schedule>> GetAllAsync();
    Task<Schedule?> GetByIdAsync(Guid scheduleId);
    Task UpdateAsync(Schedule schedule);
    Task RemoveAsync(Schedule schedule);
}