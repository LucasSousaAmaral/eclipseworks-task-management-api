namespace EW.TaskManagement.Presentation.DTOs;

public record CommentDTO
{
    public int Id { get; init; }
    public string Content { get; init; }
    public DateTime CreatedAt { get; init; }
    public int UserId { get; init; }
    public string UserName { get; init; }
}