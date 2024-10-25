namespace EW.TaskManagement.Presentation.DTOs;

public record TaskHistoryDTO
{
    public int Id { get; init; }
    public string Changes { get; init; }
    public DateTime ModifiedAt { get; init; }
    public int ModifiedByUserId { get; init; }
    public string ModifiedByUserName { get; init; }
}