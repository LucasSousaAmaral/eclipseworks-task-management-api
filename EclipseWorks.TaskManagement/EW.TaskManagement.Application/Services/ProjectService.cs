using EW.TaskManagement.Application.Interfaces;
using EW.TaskManagement.Domain.Entities;
using EW.TaskManagement.Domain.Repositories;

namespace EW.TaskManagement.Application.Services;

public class ProjectService : IProjectService
{
    private readonly IProjectRepository _projectRepository;

    public ProjectService(IProjectRepository projectRepository)
    {
        _projectRepository = projectRepository;
    }

    public async Task<IEnumerable<Project>> GetProjectsByUserIdAsync(int userId)
    {
        if (userId <= 0)
            throw new ArgumentException("O ID do usuário deve ser maior que zero.", nameof(userId));

        return await _projectRepository.GetByUserIdAsync(userId);
    }

    public async Task<Project> GetProjectByIdAsync(int projectId)
    {
        if (projectId <= 0)
            throw new ArgumentException("O ID do projeto deve ser maior que zero.", nameof(projectId));

        return await _projectRepository.GetByIdAsync(projectId);
    }

    public async Task AddProjectAsync(Project project)
    {
        if (project == null)
            throw new ArgumentNullException(nameof(project));

        await _projectRepository.AddAsync(project);
    }

    public async Task RemoveProjectAsync(int projectId)
    {
        if (projectId <= 0)
            throw new ArgumentException("O ID do projeto deve ser maior que zero.", nameof(projectId));

        var project = await _projectRepository.GetByIdAsync(projectId);
        if (project == null)
            throw new KeyNotFoundException("Projeto não encontrado.");

        if (project.HasPendingTasks())
            throw new InvalidOperationException("Não é possível remover o projeto com tarefas pendentes.");

        await _projectRepository.DeleteAsync(project);
    }
}

