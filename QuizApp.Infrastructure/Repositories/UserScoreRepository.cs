using Microsoft.EntityFrameworkCore;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Interfaces;
using QuizApp.Infrastructure.Persistence;

namespace QuizApp.Infrastructure.Repositories;

public class UserScoreRepository : IUserScoreRepository
{
    private readonly AppDbContext _context;

    public UserScoreRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(UserScore userScore)
    {
        await _context.UserScores.AddAsync(userScore);
        await _context.SaveChangesAsync();
    }

    public Task<UserScore?> GetAsync(Guid userId, Guid scheduleId)
    {
        return _context.UserScores
            .Include(us => us.User)
            .Include(us => us.Schedule)
            .Where(us => us.UserId == userId && us.ScheduleId == scheduleId)
            .FirstOrDefaultAsync();
    }

    public Task<List<UserScore>> GetByUserAsync(Guid userId)
    {
        return _context.UserScores
            .Include(us => us.Schedule)
            .ThenInclude(s => s.Quiz)
            .Include(us => us.User)
            .Where(us => us.UserId == userId)
            .ToListAsync();
    }

    public Task UpdateAsync(UserScore userScore)
    {
        _context.UserScores.Update(userScore);
        return _context.SaveChangesAsync();
    }
}