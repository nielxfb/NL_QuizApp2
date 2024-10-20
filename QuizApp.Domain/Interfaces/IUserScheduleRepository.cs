using QuizApp.Domain.Entities;

namespace QuizApp.Domain.Interfaces;

public interface IUserScheduleRepository
{
    Task AddAsync(UserSchedule userSchedule);
    Task RemoveAsync(UserSchedule userSchedule);
    Task UpdateAsync(UserSchedule userSchedule);
    Task<List<UserSchedule>> GetByUserIdAsync(UserId userId);
    Task<List<User>> GetByScheduleIdAsync(ScheduleId scheduleId);
}