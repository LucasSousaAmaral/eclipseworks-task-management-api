using EW.TaskManagement.Domain.Entities;

namespace EW.TaskManagement.Application.Interfaces;

public interface IUserService
{
    Task<User> GetByIdAsync(int userId);
    Task<bool> IsUserManagerAsync(int userId);
}