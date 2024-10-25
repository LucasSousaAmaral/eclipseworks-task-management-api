namespace EW.TaskManagement.Domain.Entities;

public class Project
{
    private const int MaxTasks = 20;

    public int Id { get; private set; }
    public string Name { get; private set; }
    public int UserId { get; private set; }
    public User User { get; private set; }
    public IReadOnlyCollection<TaskItem> Tasks => _tasks.AsReadOnly();
    private List<TaskItem> _tasks;

    protected Project()
    {
        _tasks = new List<TaskItem>();
    }

    public Project(string name, int userId)
    {
        Name = name;
        UserId = userId;
        _tasks = new List<TaskItem>();
    }

    public void AddTask(TaskItem task)
    {
        if (_tasks.Count >= MaxTasks)
            throw new InvalidOperationException("Número máximo de tarefas atingido.");

        _tasks.Add(task);
    }

    public void RemoveTask(TaskItem task)
    {
        _tasks.Remove(task);
    }

    public bool HasPendingTasks()
    {
        return _tasks.Any(t => t.Status != TaskStatus.Completed);
    }
}