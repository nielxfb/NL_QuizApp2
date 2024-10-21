using Microsoft.EntityFrameworkCore;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Interfaces;
using QuizApp.Infrastructure.Persistence;

namespace QuizApp.Infrastructure.Repositories;

public class OptionRepository : IOptionRepository
{
    private readonly AppDbContext _context;

    public OptionRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Option option)
    {
        await _context.Options.AddAsync(option);
        await _context.SaveChangesAsync();
    }

    public Task UpdateAsync(Option option)
    {
        _context.Options.Update(option);
        return _context.SaveChangesAsync();
    }

    public Task RemoveAsync(Option option)
    {
        _context.Options.Remove(option);
        return _context.SaveChangesAsync();
    }

    public Task<List<Option>> GetByQuestionIdAsync(Guid questionId)
    {
        return _context.Options
            .Where(o => o.QuestionId == questionId)
            .ToListAsync();
    }

    public Task<Option?> GetByIdAsync(Guid questionId, char optionChoice)
    {
        return _context.Options
            .Where(o => o.QuestionId == questionId
                        && o.OptionChoice == optionChoice)
            .FirstOrDefaultAsync();
    }
}