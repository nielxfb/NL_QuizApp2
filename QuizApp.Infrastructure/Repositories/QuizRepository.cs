using Microsoft.EntityFrameworkCore;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Interfaces;
using QuizApp.Infrastructure.Persistence;

namespace QuizApp.Infrastructure.Repositories;

public class QuizRepository : IQuizRepository
{
    private readonly AppDbContext _context;

    public QuizRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Quiz quiz)
    {
        await _context.Quizzes.AddAsync(quiz);
        await _context.SaveChangesAsync();
    }

    public async Task<ICollection<Quiz>> GetAllAsync()
    {
        return await _context.Quizzes.ToListAsync();
    }

    public async Task<Quiz?> GetByIdAsync(Guid quizId)
    {
        return await _context.Quizzes
            .FindAsync(quizId);
    }

    public Task UpdateAsync(Quiz quiz)
    {
        _context.Quizzes.Update(quiz);
        return _context.SaveChangesAsync();
    }

    public Task RemoveAsync(Quiz quiz)
    {
        _context.Quizzes.Remove(quiz);
        return _context.SaveChangesAsync();
    }
}