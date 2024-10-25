using System.Threading.Tasks;

namespace EW.TaskManagement.Domain.Entities;

public enum TaskStatus
{
    Pending,
    InProgress,
    Completed
}

public enum TaskPriority
{
    Low,
    Medium,
    High
}

public class TaskItem
{
    public int Id { get; private set; }
    public string Title { get; private set; }
    public string Description { get; private set; }
    public DateTime DueDate { get; private set; }
    public TaskStatus Status { get; private set; }
    public TaskPriority Priority { get; private set; }
    public int ProjectId { get; private set; }
    public Project Project { get; private set; }
    public DateTime? CompletedAt { get; private set; }
    public IReadOnlyCollection<TaskHistory> Histories => _histories.AsReadOnly();
    private List<TaskHistory> _histories;
    public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();
    private List<Comment> _comments;

    public TaskItem(string title, string description, DateTime dueDate, TaskPriority priority, int projectId)
    {
        Title = title;
        Description = description;
        DueDate = dueDate;
        Priority = priority;
        ProjectId = projectId;
        Status = TaskStatus.Pending;
        _histories = new List<TaskHistory>();
        _comments = new List<Comment>();
    }

    public TaskItem()
    {
        _histories = new List<TaskHistory>();
        _comments = new List<Comment>();
    }

    public void UpdateStatus(TaskStatus newStatus, int userId)
    {
        if (Status != newStatus)
        {
            var history = new TaskHistory(this.Id, $"Status alterado de {Status} para {newStatus}", userId);
            _histories.Add(history);
            Status = newStatus;

            if (newStatus == TaskStatus.Completed)
            {
                CompletedAt = DateTime.UtcNow;
            }
            else
            {
                CompletedAt = null;
            }
        }
    }

    public void UpdateDetails(string title, string description, DateTime dueDate, TaskPriority priority, int userId)
    {
        if (Title != title)
        {
            var history = new TaskHistory(this.Id, $"Título alterado de '{Title}' para '{title}'", userId);
            _histories.Add(history);
            Title = title;
        }

        if (Description != description)
        {
            var history = new TaskHistory(this.Id, $"Descrição alterada.", userId);
            _histories.Add(history);
            Description = description;
        }

        if (DueDate != dueDate)
        {
            var history = new TaskHistory(this.Id, $"Data de conclusão alterada de {DueDate} para {dueDate}", userId);
            _histories.Add(history);
            DueDate = dueDate;
        }

        if (Priority != priority)
        {
            var history = new TaskHistory(this.Id, $"Prioridade alterada de {Priority} para {priority}", userId);
            _histories.Add(history);
            Priority = priority;
        }
    }

    public void AddComment(string content, int userId)
    {
        if (string.IsNullOrWhiteSpace(content))
            throw new ArgumentException("O conteúdo do comentário não pode ser vazio.", nameof(content));

        var comment = new Comment(content, userId, this.Id);
        _comments.Add(comment);

        var history = new TaskHistory(this.Id, $"Comentário adicionado: {content}", userId);
        _histories.Add(history);
    }
}
