namespace EW.TaskManagement.Domain.Entities;

public class Comment
{
    public int Id { get; private set; }
    public string Content { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public int TaskItemId { get; private set; }
    public TaskItem TaskItem { get; private set; }
    public int UserId { get; private set; }
    public User User { get; private set; }

    public Comment(string content, int userId, int taskItemId)
    {
        Content = content;
        UserId = userId;
        TaskItemId = taskItemId;
        CreatedAt = DateTime.UtcNow;
    }

    public Comment()
    {
    }
}