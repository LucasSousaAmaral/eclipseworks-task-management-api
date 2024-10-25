using EW.TaskManagement.Domain.Entities;

namespace EW.TaskManagement.Application.Interfaces;

public interface IProjectService
{
    Task<IEnumerable<Project>> GetProjectsByUserIdAsync(int userId);
    Task<Project> GetProjectByIdAsync(int projectId);
    Task AddProjectAsync(Project project);
    Task RemoveProjectAsync(int projectId);
}