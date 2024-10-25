using EW.TaskManagement.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EW.TaskManagement.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportsController : ControllerBase
{
    private readonly IPerformanceReportService _reportService;

    public ReportsController(IPerformanceReportService reportService)
    {
        _reportService = reportService;
    }

    // GET: api/Reports/AverageTasksCompleted?userId=1
    [HttpGet("AverageTasksCompleted")]
    public async Task<IActionResult> GetAverageTasksCompleted([FromQuery] int userId)
    {
        if (userId <= 0)
            return BadRequest("O ID do usuário deve ser maior que zero.");

        try
        {
            var report = await _reportService.GeneratePerformanceReportAsync(userId);
            return Ok(report.AverageTasksCompletedPerUser);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Forbid(ex.Message);
        }
    }
}
