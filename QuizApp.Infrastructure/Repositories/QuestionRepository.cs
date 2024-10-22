using Microsoft.EntityFrameworkCore;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Interfaces;
using QuizApp.Infrastructure.Persistence;

namespace QuizApp.Infrastructure.Repositories;

public class QuestionRepository : IQuestionRepository
{
    private readonly AppDbContext _context;

    public QuestionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Question question)
    {
        await _context.Questions.AddAsync(question);
        await _context.SaveChangesAsync();
    }

    public Task RemoveAsync(Question question)
    {
        _context.Questions.Remove(question);
        return _context.SaveChangesAsync();
    }

    public Task UpdateAsync(Question question)
    {
        _context.Questions.Update(question);
        return _context.SaveChangesAsync();
    }

    public Task<List<Question>> GetByQuizIdAsync(Guid quizId)
    {
        return _context.Questions
            .Include(q => q.Options)
            .Where(q => q.QuizId == quizId)
            .ToListAsync();
    }

    public async Task<Question?> GetByIdAsync(Guid questionId)
    {
        return await _context.Questions.FindAsync(questionId);
    }
}