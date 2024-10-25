using EW.TaskManagement.Domain.Entities;

namespace EW.TaskManagement.Presentation.DTOs;

public record CreateTaskItemDTO
{
    public string Title { get; init; }
    public string Description { get; init; }
    public DateTime DueDate { get; init; }
    public TaskPriority Priority { get; init; }
    public int ProjectId { get; init; }
}