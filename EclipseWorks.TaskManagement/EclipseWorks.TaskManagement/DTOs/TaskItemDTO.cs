using EW.TaskManagement.Domain.Entities;

namespace EW.TaskManagement.Presentation.DTOs;

public record TaskItemDTO
{
    public int Id { get; init; }
    public string Title { get; init; }
    public int ProjectId { get; init; }
    public TaskPriority Priority { get; init; }
    public DateTime DueDate { get; init; }
    public List<CommentDTO> Comments { get; init; }
    public List<TaskHistoryDTO> Histories { get; init; }
}