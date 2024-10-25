using EW.TaskManagement.Domain.Entities;

namespace EW.TaskManagement.Presentation.DTOs;

public record UpdateTaskItemDTO
{
    public int Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public DateTime DueDate { get; init; }
    public TaskPriority Priority { get; init; }
    public Domain.Entities.TaskStatus Status { get; init; }
}