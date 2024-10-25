using EW.TaskManagement.Domain.Entities;

namespace EW.TaskManagement.Presentation.DTOs;

public class UpdateTaskItemDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public TaskPriority Priority { get; set; }
    public Domain.Entities.TaskStatus Status { get; set; }
}