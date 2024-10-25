namespace EW.TaskManagement.Presentation.DTOs;

public record CreateProjectDTO
{
    public string Name { get; init; }
    public int UserId { get; init; }
}