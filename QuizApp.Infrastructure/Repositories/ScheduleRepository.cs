using Microsoft.EntityFrameworkCore;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Interfaces;
using QuizApp.Infrastructure.Persistence;

namespace QuizApp.Infrastructure.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly AppDbContext _context;

    public ScheduleRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Schedule schedule)
    {
        await _context.Schedules.AddAsync(schedule);
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<Schedule>> GetAllAsync()
    {
        return await _context.Schedules
            .Include(s => s.Quiz)
            .ToListAsync();
    }

    public async Task<Schedule?> GetByIdAsync(Guid scheduleId)
    {
        return await _context.Schedules
            .Include(s => s.Quiz)
            .Include(s => s.UserSchedules)
            .Where(s => s.ScheduleId == scheduleId)
            .FirstOrDefaultAsync();
    }

    public Task UpdateAsync(Schedule schedule)
    {
        _context.Schedules.Update(schedule);
        return _context.SaveChangesAsync();
    }

    public Task RemoveAsync(Schedule schedule)
    {
        _context.Schedules.Remove(schedule);
        return _context.SaveChangesAsync();
    }
}