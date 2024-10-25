namespace EW.TaskManagement.Domain.Entities;

public class PerformanceReport
{
    public double AverageTasksCompletedPerUser { get; set; }
    public int TotalTasksCompleted { get; set; }
    public int TotalUsers { get; set; }
    public DateTime GeneratedAt { get; set; }

    public PerformanceReport()
    {
        GeneratedAt = DateTime.UtcNow;
    }
}