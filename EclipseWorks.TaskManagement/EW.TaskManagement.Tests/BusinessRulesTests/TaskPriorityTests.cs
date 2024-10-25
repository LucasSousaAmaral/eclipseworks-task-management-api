using EW.TaskManagement.Domain.Entities;

namespace EW.TaskManagement.Tests.BusinessRulesTests;

public class TaskPriorityTests
{
    [Fact]
    public void TaskItem_Should_Set_Priority_On_Creation()
    {
        // Arrange
        var title = "Título da Tarefa";
        var description = "Descrição da Tarefa";
        var dueDate = DateTime.UtcNow.AddDays(5);
        var priority = TaskPriority.High;
        var projectId = 1;

        // Act
        var taskItem = new TaskItem(title, description, dueDate, priority, projectId);

        // Assert
        Assert.Equal(priority, taskItem.Priority);
    }

    [Fact]
    public void TaskItem_Priority_Should_Not_Be_Modifiable()
    {
        // Arrange
        var taskItemType = typeof(TaskItem);
        var priorityProperty = taskItemType.GetProperty("Priority");

        // Act
        var hasPrivateSetter = priorityProperty.SetMethod != null && priorityProperty.SetMethod.IsPrivate;

        // Assert
        Assert.True(hasPrivateSetter);
    }
}