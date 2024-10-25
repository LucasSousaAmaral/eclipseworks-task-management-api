using EW.TaskManagement.Domain.Entities;

namespace EW.TaskManagement.Application.Interfaces;

public interface ITaskItemService
{
    Task<IEnumerable<TaskItem>> GetTasksByProjectIdAsync(int projectId);
    Task<TaskItem> GetTaskByIdAsync(int taskId);
    Task AddTaskAsync(TaskItem taskItem);
    Task UpdateTaskAsync(TaskItem updatedTask, int userId);
    Task DeleteTaskAsync(int taskId);
    Task AddCommentAsync(int taskId, string content, int userId);
}