using EW.TaskManagement.Application.Interfaces;
using EW.TaskManagement.Domain.Entities;
using EW.TaskManagement.Domain.Repositories;

namespace EW.TaskManagement.Application.Servicess;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> GetByIdAsync(int userId)
    {
        if (userId <= 0)
            throw new ArgumentException("O ID do usuário deve ser maior que zero.", nameof(userId));

        return await _userRepository.GetByIdAsync(userId);
    }

    public async Task<bool> IsUserManagerAsync(int userId)
    {
        if (userId <= 0)
            throw new ArgumentException("O ID do usuário deve ser maior que zero.", nameof(userId));

        var user = await _userRepository.GetByIdAsync(userId);
        return user != null && user.Role == UserRole.Manager;
    }
}
