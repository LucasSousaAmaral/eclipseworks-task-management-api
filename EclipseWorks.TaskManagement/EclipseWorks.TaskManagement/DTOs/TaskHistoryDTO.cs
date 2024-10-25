namespace EW.TaskManagement.Presentation.DTOs;

public class TaskHistoryDTO
{
    public int Id { get; set; }
    public string Changes { get; set; }
    public DateTime ModifiedAt { get; set; }
    public int ModifiedByUserId { get; set; }
    public string ModifiedByUserName { get; set; }
}