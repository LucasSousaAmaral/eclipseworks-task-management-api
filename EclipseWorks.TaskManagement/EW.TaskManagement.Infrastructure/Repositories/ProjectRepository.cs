using EW.TaskManagement.Domain.Entities;
using EW.TaskManagement.Domain.Repositories;
using EW.TaskManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EW.TaskManagement.Infrastructure.Repositories;

public class ProjectRepository : IProjectRepository
{
    private readonly AppDbContext _context;

    public ProjectRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(Project project)
    {
        await _context.Projects.AddAsync(project);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Project project)
    {
        _context.Projects.Remove(project);
        await _context.SaveChangesAsync();
    }

    public async Task<Project> GetByIdAsync(int id)
    {
        return await _context.Projects
            .Include(p => p.User)
            .Include(p => p.Tasks)
                .ThenInclude(t => t.Comments)
                    .ThenInclude(c => c.User)
            .Include(p => p.Tasks)
                .ThenInclude(t => t.Histories)
                    .ThenInclude(h => h.ModifiedByUser)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Project>> GetByUserIdAsync(int userId)
    {
        return await _context.Projects
            .Where(p => p.UserId == userId)
            .Include(p => p.Tasks)
            .ToListAsync();
    }

    public async Task UpdateAsync(Project project)
    {
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
    }
}
