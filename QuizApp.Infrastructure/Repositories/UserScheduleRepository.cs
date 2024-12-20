using Microsoft.EntityFrameworkCore;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Interfaces;
using QuizApp.Infrastructure.Persistence;

namespace QuizApp.Infrastructure.Repositories;

public class UserScheduleRepository : IUserScheduleRepository
{
    private readonly AppDbContext _context;

    public UserScheduleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(UserSchedule userSchedule)
    {
        await _context.UserSchedules.AddAsync(userSchedule);
        await _context.SaveChangesAsync();
    }

    public Task RemoveAsync(UserSchedule userSchedule)
    {
        _context.UserSchedules.Remove(userSchedule);
        return _context.SaveChangesAsync();
    }

    public Task UpdateAsync(UserSchedule userSchedule)
    {
        _context.UserSchedules.Update(userSchedule);
        return _context.SaveChangesAsync();
    }

    public async Task<List<UserSchedule>> GetByUserIdAsync(Guid userId)
    {
        return await _context.UserSchedules
            .Include(us => us.User)
            .Include(us => us.Schedule)
            .ThenInclude(s => s.Quiz)
            .Where(us => us.UserId == userId)
            .ToListAsync();
    }

    public async Task<List<User>> GetByScheduleIdAsync(Guid scheduleId)
    {
        return await _context.Users
            .Include(u => u.UserSchedules)
            .Where(u => u.UserSchedules.Any(us => us.ScheduleId == scheduleId))
            .ToListAsync();
    }

    public async Task<UserSchedule?> GetByScheduleAndUserId(Guid userId, Guid scheduleId)
    {
        return await _context.UserSchedules
            .FirstOrDefaultAsync(us => us.UserId == userId && us.ScheduleId == scheduleId);
    }
}