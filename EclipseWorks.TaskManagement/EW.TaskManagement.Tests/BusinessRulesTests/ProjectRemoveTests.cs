using EW.TaskManagement.Application.Interfaces;
using EW.TaskManagement.Application.Services;
using EW.TaskManagement.Domain.Entities;
using EW.TaskManagement.Domain.Repositories;
using Moq;

namespace EW.TaskManagement.Tests.BusinessRulesTests;

public class ProjectRemoveTests
{
    private readonly Mock<IProjectRepository> _projectRepositoryMock;
    private readonly Mock<IUserService> _userServiceMock;
    private readonly IProjectService _projectService;

    public ProjectRemoveTests()
    {
        _projectRepositoryMock = new Mock<IProjectRepository>();
        _userServiceMock = new Mock<IUserService>();

        _projectService = new ProjectService(
            _projectRepositoryMock.Object
        );
    }

    [Fact]
    public async Task RemoveProjectAsync_Should_Throw_Exception_When_Project_Has_Pending_Tasks()
    {
        // Arrange
        var projectId = 1;
        var project = new Project("Projeto Teste", 1);
        var pendingTask = new TaskItem("Tarefa Pendente", "Descrição", DateTime.UtcNow.AddDays(1), TaskPriority.Medium, projectId);

        project.AddTask(pendingTask);

        _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(projectId)).ReturnsAsync(project);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<InvalidOperationException>(() => _projectService.RemoveProjectAsync(projectId));
        Assert.Equal("Não é possível remover o projeto com tarefas pendentes.", exception.Message);
    }

    [Fact]
    public async Task RemoveProjectAsync_Should_Remove_Project_When_No_Pending_Tasks()
    {
        // Arrange
        var projectId = 1;
        var project = new Project("Projeto Teste", 1);

        _projectRepositoryMock.Setup(repo => repo.GetByIdAsync(projectId)).ReturnsAsync(project);
        _projectRepositoryMock.Setup(repo => repo.DeleteAsync(project)).Returns(Task.CompletedTask);

        // Act
        await _projectService.RemoveProjectAsync(projectId);

        // Assert
        _projectRepositoryMock.Verify(repo => repo.DeleteAsync(project), Times.Once);
    }
}
