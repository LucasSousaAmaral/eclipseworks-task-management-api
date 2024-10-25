using EW.TaskManagement.Domain.Entities;

namespace EW.TaskManagement.Domain.Repositories;

public interface IProjectRepository
{
    Task<Project> GetByIdAsync(int id);
    Task<IEnumerable<Project>> GetByUserIdAsync(int userId);
    Task AddAsync(Project project);
    Task UpdateAsync(Project project);
    Task DeleteAsync(Project project);
}