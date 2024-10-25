using EW.TaskManagement.Application.Interfaces;
using EW.TaskManagement.Application.Services;
using EW.TaskManagement.Domain.Entities;
using EW.TaskManagement.Domain.Repositories;
using Moq;

namespace EW.TaskManagement.Tests.BusinessRulesTests;

public class ReportTests
{

    private readonly Mock<ITaskItemRepository> _taskRepositoryMock;
    private readonly Mock<IUserService> _userServiceMock;
    private readonly PerformanceReportService _performanceReportService;
    public ReportTests()
    {
        _taskRepositoryMock = new Mock<ITaskItemRepository>();
        _userServiceMock = new Mock<IUserService>();

        _performanceReportService = new PerformanceReportService(
            _taskRepositoryMock.Object,
            _userServiceMock.Object
        );
    }

    [Fact]
    public async Task GeneratePerformanceReportAsync_Should_Return_Report_For_Manager()
    {
        // Arrange
        var userId = 1;
        _userServiceMock.Setup(u => u.IsUserManagerAsync(userId)).ReturnsAsync(true);

        var completedTasks = new List<TaskItem>
        {
            // Mock de tarefas concluídas com históricos apropriados
        };

        _taskRepositoryMock.Setup(r => r.GetCompletedTasksWithHistoriesSinceAsync(It.IsAny<DateTime>())).ReturnsAsync(completedTasks);

        // Act
        var report = await _performanceReportService.GeneratePerformanceReportAsync(userId);

        // Assert
        Assert.NotNull(report);
        // Verificar os valores do relatório conforme os dados de teste
    }

    [Fact]
    public async Task GeneratePerformanceReportAsync_Should_Throw_Exception_For_Non_Manager()
    {
        // Arrange
        var userId = 2;
        _userServiceMock.Setup(u => u.IsUserManagerAsync(userId)).ReturnsAsync(false);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<UnauthorizedAccessException>(() => _performanceReportService.GeneratePerformanceReportAsync(userId));
        Assert.Equal("Usuário não tem permissão para gerar este relatório.", exception.Message);
    }
}
