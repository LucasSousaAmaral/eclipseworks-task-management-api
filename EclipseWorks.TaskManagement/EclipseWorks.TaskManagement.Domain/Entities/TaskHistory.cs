namespace EW.TaskManagement.Domain.Entities;

public class TaskHistory
{
    public int Id { get; private set; }
    public int TaskItemId { get; private set; }
    public TaskItem TaskItem { get; private set; }
    public string Changes { get; private set; }
    public DateTime ModifiedAt { get; private set; }
    public int ModifiedByUserId { get; private set; }
    public User ModifiedByUser { get; private set; }

    public TaskHistory(int taskItemId, string changes, int modifiedByUserId)
    {
        TaskItemId = taskItemId;
        Changes = changes;
        ModifiedAt = DateTime.UtcNow;
        ModifiedByUserId = modifiedByUserId;
    }

    public TaskHistory()
    {
    }
}