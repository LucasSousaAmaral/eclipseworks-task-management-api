using EW.TaskManagement.Domain.Entities;

namespace EW.TaskManagement.Tests.BusinessRulesTests;

public class TaskHistoryTests
{
    [Fact]
    public void UpdateStatus_Should_Record_History_When_Status_Is_Updated()
    {
        // Arrange
        var taskItem = new TaskItem("Título", "Descrição", DateTime.UtcNow.AddDays(1), TaskPriority.Medium, 1);
        var userId = 1;
        var newStatus = Domain.Entities.TaskStatus.InProgress;

        // Act
        taskItem.UpdateStatus(newStatus, userId);

        // Assert
        Assert.Single(taskItem.Histories);
        var history = taskItem.Histories.First();
        Assert.Contains($"Status alterado de {Domain.Entities.TaskStatus.Pending} para {newStatus}", history.Changes);
        Assert.Equal(userId, history.ModifiedByUserId);
        Assert.True((DateTime.UtcNow - history.ModifiedAt).TotalSeconds < 1);
    }

    [Fact]
    public void UpdateDetails_Should_Record_History_When_Details_Are_Updated()
    {
        // Arrange
        var taskItem = new TaskItem("Título Antigo", "Descrição Antiga", DateTime.UtcNow.AddDays(1), TaskPriority.Medium, 1);
        var userId = 1;
        var newTitle = "Título Novo";
        var newDescription = "Descrição Nova";
        var newDueDate = DateTime.UtcNow.AddDays(2);
        var newPriority = TaskPriority.High;

        // Act
        taskItem.UpdateDetails(newTitle, newDescription, newDueDate, newPriority, userId);

        // Assert
        Assert.Equal(4, taskItem.Histories.Count);
        var titlesChanged = taskItem.Histories.Any(h => h.Changes.Contains("Título alterado de"));
        var descriptionsChanged = taskItem.Histories.Any(h => h.Changes.Contains("Descrição alterada."));
        var dueDateChanged = taskItem.Histories.Any(h => h.Changes.Contains("Data de conclusão alterada de"));
        var priorityChanged = taskItem.Histories.Any(h => h.Changes.Contains("Prioridade alterada de"));

        Assert.True(titlesChanged);
        Assert.True(descriptionsChanged);
        Assert.True(dueDateChanged);
        Assert.True(priorityChanged);
    }
}
