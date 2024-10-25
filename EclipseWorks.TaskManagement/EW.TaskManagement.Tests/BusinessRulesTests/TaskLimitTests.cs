using EW.TaskManagement.Domain.Entities;

namespace EW.TaskManagement.Tests.BusinessRulesTests;

public class TaskLimitTests
{
    [Fact]
    public void AddTask_Should_Allow_Adding_Tasks_Up_To_Limit()
    {
        // Arrange
        var project = new Project("Projeto Teste", 1);

        // Act
        for (int i = 0; i < 20; i++)
        {
            project.AddTask(new TaskItem($"Tarefa {i}", "Descrição", DateTime.UtcNow.AddDays(1), TaskPriority.Medium, project.Id));
        }

        // Assert
        Assert.Equal(20, project.Tasks.Count);
    }

    [Fact]
    public void AddTask_Should_Throw_Exception_When_Exceeding_Task_Limit()
    {
        // Arrange
        var project = new Project("Projeto Teste", 1);

        for (int i = 0; i < 20; i++)
        {
            project.AddTask(new TaskItem($"Tarefa {i}", "Descrição", DateTime.UtcNow.AddDays(1), TaskPriority.Medium, project.Id));
        }

        var extraTask = new TaskItem("Tarefa Extra", "Descrição", DateTime.UtcNow.AddDays(1), TaskPriority.Medium, project.Id);

        // Act & Assert
        var exception = Assert.Throws<InvalidOperationException>(() => project.AddTask(extraTask));
        Assert.Equal("Número máximo de tarefas atingido.", exception.Message);
    }
}
