using EW.TaskManagement.Domain.Entities;

namespace EW.TaskManagement.Domain.Repositories;

public interface IUserRepository
{
    Task<User> GetByIdAsync(int userId);
    Task<bool> IsUserManagerAsync(int userId);
}