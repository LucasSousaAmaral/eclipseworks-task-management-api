using AutoMapper;
using EW.TaskManagement.Application.Interfaces;
using EW.TaskManagement.Domain.Entities;
using EW.TaskManagement.Presentation.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EW.TaskManagement.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjectsController : ControllerBase
{
    private readonly IProjectService _projectService;
    private readonly IMapper _mapper;

    public ProjectsController(IProjectService projectService, IMapper mapper)
    {
        _projectService = projectService;
        _mapper = mapper;
    }

    // GET: api/Projects?userId=1
    [HttpGet]
    public async Task<IActionResult> GetProjects([FromQuery] int userId)
    {
        if (userId <= 0)
            return BadRequest("O ID do usuário deve ser maior que zero.");

        var projects = await _projectService.GetProjectsByUserIdAsync(userId);

        var projectDTOs = _mapper.Map<IEnumerable<ProjectDTO>>(projects);

        return Ok(projectDTOs);
    }

    // GET: api/Projects/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetProjectById(int id)
    {
        if (id <= 0)
            return BadRequest("O ID do projeto deve ser maior que zero.");

        var project = await _projectService.GetProjectByIdAsync(id);
        if (project == null)
            return NotFound("Projeto não encontrado.");

        var projectDTO = _mapper.Map<ProjectDTO>(project);

        return Ok(projectDTO);
    }

    // POST: api/Projects
    [HttpPost]
    public async Task<IActionResult> CreateProject([FromBody] CreateProjectDTO createProjectDTO)
    {
        if (createProjectDTO == null)
            return BadRequest("Dados do projeto são obrigatórios.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var project = _mapper.Map<Project>(createProjectDTO);

        await _projectService.AddProjectAsync(project);

        var projectDTO = _mapper.Map<ProjectDTO>(project);

        return CreatedAtAction(nameof(GetProjectById), new { id = projectDTO.Id }, projectDTO);
    }

    // DELETE: api/Projects/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProject(int id)
    {
        if (id <= 0)
            return BadRequest("O ID do projeto deve ser maior que zero.");

        try
        {
            await _projectService.RemoveProjectAsync(id);
            return NoContent();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}
