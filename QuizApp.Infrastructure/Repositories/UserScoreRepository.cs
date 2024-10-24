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
}