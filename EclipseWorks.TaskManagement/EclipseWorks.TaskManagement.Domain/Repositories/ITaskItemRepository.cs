using EW.TaskManagement.Domain.Entities;

namespace EW.TaskManagement.Domain.Repositories;

public interface ITaskItemRepository
{
    Task<TaskItem> GetByIdAsync(int id);
    Task<IEnumerable<TaskItem>> GetByProjectIdAsync(int projectId);
    Task AddAsync(TaskItem taskItem);
    Task UpdateAsync(TaskItem taskItem);
    Task DeleteAsync(TaskItem taskItem);
    Task<IEnumerable<TaskItem>> GetCompletedTasksSinceAsync(DateTime sinceDate);
    Task<IEnumerable<TaskItem>> GetCompletedTasksWithHistoriesSinceAsync(DateTime sinceDate);
}