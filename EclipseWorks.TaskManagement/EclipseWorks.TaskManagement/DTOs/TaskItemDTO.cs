using EW.TaskManagement.Domain.Entities;

namespace EW.TaskManagement.Presentation.DTOs;

public class TaskItemDTO
{
    public int Id { get; set; }
    public string Title { get; set; }
    public int ProjectId { get; set; }
    public TaskPriority Priority { get; set; }
    public DateTime DueDate { get; set; }
    public List<CommentDTO> Comments { get; set; }
    public List<TaskHistoryDTO> Histories { get; set; }
}