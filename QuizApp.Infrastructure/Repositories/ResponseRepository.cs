using Microsoft.EntityFrameworkCore;
using QuizApp.Domain.Entities;
using QuizApp.Domain.Interfaces;
using QuizApp.Infrastructure.Persistence;

namespace QuizApp.Infrastructure.Repositories;

public class ResponseRepository : IResponseRepository
{
    private readonly AppDbContext _context;

    public ResponseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Response response)
    {
        await _context.Responses.AddAsync(response);
        await _context.SaveChangesAsync();
    }

    public Task UpdateAsync(Response response)
    {
        _context.Responses.Update(response);
        return _context.SaveChangesAsync();
    }

    public async Task<List<Response>> GetUserResponsesInQuizAsync(Guid userId, Guid quizId)
    {
        return await _context.Responses
            .Where(r => r.UserId == userId && r.QuizId == quizId)
            .Include(r => r.Option)
            .Include(r => r.Question)
            .ToListAsync();
    }

    public Task<Response?> GetByIdAsync(Guid responseId)
    {
        return _context.Responses
            .FirstOrDefaultAsync(r => r.ResponseId == responseId);
    }

    public Task<Response?> GetExistingResponse(Guid userId, Guid questionId)
    {
        return _context.Responses
            .Where(r => r.UserId == userId && r.QuestionId == questionId)
            .FirstOrDefaultAsync();
    }
}