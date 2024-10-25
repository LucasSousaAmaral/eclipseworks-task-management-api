namespace EW.TaskManagement.Presentation.DTOs;

public class CommentDTO
{
    public int Id { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; }
    public int UserId { get; set; }
    public string UserName { get; set; }
}