namespace EW.TaskManagement.Domain.Entities;

public class User
{
    public int Id { get; private set; }
    public string Name { get; private set; }
    public UserRole Role { get; private set; }

    private List<Project> _projects;
    public IReadOnlyCollection<Project> Projects => _projects.AsReadOnly();

    private List<Comment> _comments;
    public IReadOnlyCollection<Comment> Comments => _comments.AsReadOnly();

    private List<TaskHistory> _taskHistories;
    public IReadOnlyCollection<TaskHistory> TaskHistories => _taskHistories.AsReadOnly();

    public User(int id, string name, UserRole role)
    {
        Id = id;
        Name = name;
        Role = role;

        _projects = new List<Project>();
        _comments = new List<Comment>();
        _taskHistories = new List<TaskHistory>();
    }

    public User()
    {
        _projects = new List<Project>();
        _comments = new List<Comment>();
        _taskHistories = new List<TaskHistory>();
    }
}

public enum UserRole
{
    RegularUser,
    Manager
}