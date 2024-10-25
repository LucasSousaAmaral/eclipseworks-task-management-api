using EW.TaskManagement.Domain.Entities;
using EW.TaskManagement.Domain.Repositories;
using EW.TaskManagement.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EW.TaskManagement.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;

    public UserRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<User> GetByIdAsync(int userId)
    {
        return await _context.Users
            .Include(u => u.Projects)
            .Include(u => u.Comments)
            .Include(u => u.TaskHistories)
            .FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<bool> IsUserManagerAsync(int userId)
    {
        var user = await _context.Users.FindAsync(userId);
        return user != null && user.Role == UserRole.Manager;
    }
}
