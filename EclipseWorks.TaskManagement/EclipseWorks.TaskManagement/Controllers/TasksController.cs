using AutoMapper;
using EW.TaskManagement.Application.Interfaces;
using EW.TaskManagement.Domain.Entities;
using EW.TaskManagement.Presentation.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace EW.TaskManagement.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskItemService _taskItemService;
    private readonly IMapper _mapper;

    public TasksController(ITaskItemService taskItemService, IMapper mapper)
    {
        _taskItemService = taskItemService;
        _mapper = mapper;
    }

    // GET: api/Tasks/Project/5
    [HttpGet("Project/{projectId}")]
    public async Task<IActionResult> GetTasksByProject(int projectId)
    {
        if (projectId <= 0)
            return BadRequest("O ID do projeto deve ser maior que zero.");

        var tasks = await _taskItemService.GetTasksByProjectIdAsync(projectId);

        var taskDTOs = _mapper.Map<IEnumerable<TaskItemDTO>>(tasks);

        return Ok(taskDTOs);
    }

    // GET: api/Tasks/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetTaskById(int id)
    {
        if (id <= 0)
            return BadRequest("O ID da tarefa deve ser maior que zero.");

        var task = await _taskItemService.GetTaskByIdAsync(id);
        if (task == null)
            return NotFound("Tarefa não encontrada.");

        var taskDTO = _mapper.Map<TaskItemDTO>(task);

        return Ok(taskDTO);
    }

    // POST: api/Tasks
    [HttpPost]
    public async Task<IActionResult> CreateTask([FromBody] CreateTaskItemDTO createTaskItemDTO)
    {
        if (createTaskItemDTO == null)
            return BadRequest("Dados da tarefa são obrigatórios.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var taskItem = _mapper.Map<TaskItem>(createTaskItemDTO);

        try
        {
            await _taskItemService.AddTaskAsync(taskItem);

            var taskDTO = _mapper.Map<TaskItemDTO>(taskItem);

            return CreatedAtAction(nameof(GetTaskById), new { id = taskDTO.Id }, taskDTO);
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

    // PUT: api/Tasks/5?userId=1
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateTask(int id, [FromBody] UpdateTaskItemDTO updateTaskItemDTO, [FromQuery] int userId)
    {
        if (updateTaskItemDTO == null)
            return BadRequest("Dados da tarefa são obrigatórios.");

        if (id != updateTaskItemDTO.Id)
            return BadRequest("O ID da tarefa na URL não corresponde ao ID no corpo da requisição.");

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var taskItem = _mapper.Map<TaskItem>(updateTaskItemDTO);

        try
        {
            await _taskItemService.UpdateTaskAsync(taskItem, userId);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    // DELETE: api/Tasks/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteTask(int id)
    {
        if (id <= 0)
            return BadRequest("O ID da tarefa deve ser maior que zero.");

        try
        {
            await _taskItemService.DeleteTaskAsync(id);
            return NoContent();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }

    // POST: api/Tasks/5/Comments?userId=1
    [HttpPost("{id}/Comments")]
    public async Task<IActionResult> AddComment(int id, [FromBody] string content, [FromQuery] int userId)
    {
        if (string.IsNullOrWhiteSpace(content))
            return BadRequest("O conteúdo do comentário não pode ser vazio.");

        if (userId <= 0)
            return BadRequest("O ID do usuário deve ser maior que zero.");

        try
        {
            await _taskItemService.AddCommentAsync(id, content, userId);
            return Ok();
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(ex.Message);
        }
    }
}