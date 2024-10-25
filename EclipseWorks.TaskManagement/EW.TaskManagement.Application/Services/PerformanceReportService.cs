using EW.TaskManagement.Application.Interfaces;
using EW.TaskManagement.Domain.Entities;
using EW.TaskManagement.Domain.Repositories;

namespace EW.TaskManagement.Application.Services;

public class PerformanceReportService : IPerformanceReportService
{
    private readonly ITaskItemRepository _taskRepository;
    private readonly IUserService _userService;

    public PerformanceReportService(ITaskItemRepository taskRepository, IUserService userService)
    {
        _taskRepository = taskRepository;
        _userService = userService;
    }

    public async Task<PerformanceReport> GeneratePerformanceReportAsync(int userId)
    {
        if (userId <= 0)
            throw new ArgumentException("O ID do usuário deve ser maior que zero.", nameof(userId));

        if (!await _userService.IsUserManagerAsync(userId))
        {
            throw new UnauthorizedAccessException("Usuário não tem permissão para gerar este relatório.");
        }

        DateTime thirtyDaysAgo = DateTime.UtcNow.AddDays(-30);

        // Obter tarefas concluídas nos últimos 30 dias, incluindo os históricos
        var completedTasks = await _taskRepository.GetCompletedTasksWithHistoriesSinceAsync(thirtyDaysAgo);

        int totalTasksCompleted = completedTasks.Count();

        var userIds = new HashSet<int>();

        foreach (var task in completedTasks)
        {
            var completionHistory = task.Histories
                .Where(h => h.Changes.Contains($"Status alterado de") &&
                            h.Changes.Contains($"para {Domain.Entities.TaskStatus.Completed}") &&
                            h.ModifiedAt >= thirtyDaysAgo)
                .FirstOrDefault();

            if (completionHistory != null)
            {
                userIds.Add(completionHistory.ModifiedByUserId);
            }
        }

        int totalUsers = userIds.Count;

        double averageTasksCompletedPerUser = totalUsers > 0
            ? (double)totalTasksCompleted / totalUsers
            : 0;

        var report = new PerformanceReport
        {
            AverageTasksCompletedPerUser = averageTasksCompletedPerUser,
            TotalTasksCompleted = totalTasksCompleted,
            TotalUsers = totalUsers,
            GeneratedAt = DateTime.UtcNow
        };

        return report;
    }
}