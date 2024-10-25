using EW.TaskManagement.Domain.Entities;

namespace EW.TaskManagement.Application.Interfaces;

public interface IPerformanceReportService
{
    Task<PerformanceReport> GeneratePerformanceReportAsync(int userId);
}