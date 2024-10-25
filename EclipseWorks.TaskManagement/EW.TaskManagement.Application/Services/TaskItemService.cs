using EW.TaskManagement.Application.Interfaces;
using EW.TaskManagement.Domain.Entities;
using EW.TaskManagement.Domain.Repositories;

namespace EW.TaskManagement.Application.Services;

public class TaskItemService : ITaskItemService
{
    private readonly ITaskItemRepository _taskItemRepository;
    private readonly IProjectRepository _projectRepository;

    public TaskItemService(ITaskItemRepository taskItemRepository, IProjectRepository projectRepository)
    {
        _taskItemRepository = taskItemRepository;
        _projectRepository = projectRepository;
    }

    public async Task<IEnumerable<TaskItem>> GetTasksByProjectIdAsync(int projectId)
    {
        if (projectId <= 0)
            throw new ArgumentException("O ID do projeto deve ser maior que zero.", nameof(projectId));

        return await _taskItemRepository.GetByProjectIdAsync(projectId);
    }
    public async Task<TaskItem> GetTaskByIdAsync(int taskId)
    {
        if (taskId <= 0)
            throw new ArgumentException("O ID do projeto deve ser maior que zero.", nameof(taskId));

        return await _taskItemRepository.GetByIdAsync(taskId);
    }

    public async Task AddTaskAsync(TaskItem taskItem)
    {
        if (taskItem == null)
            throw new ArgumentNullException(nameof(taskItem));

        var project = await _projectRepository.GetByIdAsync(taskItem.ProjectId);
        if (project == null)
            throw new KeyNotFoundException("Projeto não encontrado.");

        project.AddTask(taskItem);

        await _projectRepository.UpdateAsync(project);
    }

    public async Task UpdateTaskAsync(TaskItem updatedTask, int userId)
    {
        if (updatedTask == null)
            throw new ArgumentNullException(nameof(updatedTask));

        if (userId <= 0)
            throw new ArgumentException("O ID do usuário deve ser maior que zero.", nameof(userId));

        var taskItem = await _taskItemRepository.GetByIdAsync(updatedTask.Id);
        if (taskItem == null)
            throw new KeyNotFoundException("Tarefa não encontrada.");

        taskItem.UpdateStatus(updatedTask.Status, userId);

        taskItem.UpdateDetails(
            updatedTask.Title,
            updatedTask.Description,
            updatedTask.DueDate,
            updatedTask.Priority,
            userId
        );

        await _taskItemRepository.UpdateAsync(taskItem);
    }

    public async Task DeleteTaskAsync(int taskId)
    {
        if (taskId <= 0)
            throw new ArgumentException("O ID da tarefa deve ser maior que zero.", nameof(taskId));

        var taskItem = await _taskItemRepository.GetByIdAsync(taskId);
        if (taskItem == null)
            throw new KeyNotFoundException("Tarefa não encontrada.");

        await _taskItemRepository.DeleteAsync(taskItem);
    }

    public async Task AddCommentAsync(int taskId, string content, int userId)
    {
        if (taskId <= 0)
            throw new ArgumentException("O ID da tarefa deve ser maior que zero.", nameof(taskId));

        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("O conteúdo do comentário não pode ser vazio.", nameof(content));

        if (userId <= 0)
            throw new ArgumentException("O ID do usuário deve ser maior que zero.", nameof(userId));

        var taskItem = await _taskItemRepository.GetByIdAsync(taskId);
        if (taskItem == null)
            throw new KeyNotFoundException("Tarefa não encontrada.");

        taskItem.AddComment(content, userId);
        await _taskItemRepository.UpdateAsync(taskItem);
    }
}
