namespace EW.TaskManagement.Presentation.DTOs;

public record ProjectDTO
{
    public int Id { get; init; }
    public string Name { get; init; }
    public int UserId { get; init; }
}