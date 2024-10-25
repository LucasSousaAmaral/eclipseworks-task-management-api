using EW.TaskManagement.Domain.Entities;
using EW.TaskManagement.Domain.Repositories;
using EW.TaskManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EW.TaskManagement.Infrastructure.Repositories;

public class TaskItemRepository : ITaskItemRepository
{
    private readonly AppDbContext _context;

    public TaskItemRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(TaskItem taskItem)
    {
        await _context.TaskItems.AddAsync(taskItem);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TaskItem taskItem)
    {
        _context.TaskItems.Remove(taskItem);
        await _context.SaveChangesAsync();
    }

    public async Task<TaskItem> GetByIdAsync(int id)
    {
        return await _context.TaskItems
            .Include(t => t.Project)
                .ThenInclude(p => p.User)
            .Include(t => t.Comments)
                .ThenInclude(c => c.User)
            .Include(t => t.Histories)
                .ThenInclude(h => h.ModifiedByUser)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<IEnumerable<TaskItem>> GetByProjectIdAsync(int projectId)
    {
        return await _context.TaskItems
            .Where(t => t.ProjectId == projectId)
            .Include(t => t.Comments)
                .ThenInclude(c => c.User)
            .Include(t => t.Histories)
                .ThenInclude(h => h.ModifiedByUser)
            .ToListAsync();
    }

    public async Task UpdateAsync(TaskItem taskItem)
    {
        _context.TaskItems.Update(taskItem);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<TaskItem>> GetCompletedTasksSinceAsync(DateTime sinceDate)
    {
        return await _context.TaskItems
            .Where(t => t.CompletedAt != null && t.CompletedAt >= sinceDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<TaskItem>> GetCompletedTasksWithHistoriesSinceAsync(DateTime sinceDate)
    {
        return await _context.TaskItems
            .Include(t => t.Histories)
            .Where(t => t.Status == Domain.Entities.TaskStatus.Completed && t.CompletedAt >= sinceDate)
            .ToListAsync();
    }
}
