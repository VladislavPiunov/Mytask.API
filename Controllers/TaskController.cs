using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mytask.API.Model;
using Task = Mytask.API.Model.Task;

namespace Mytask.API.Controllers;

[ApiController]
[Route("api/task")]
public class TaskController : ControllerBase
{
    private readonly ITaskRepository _taskRepository;

    public TaskController(
        ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    [Route("{boardId}")]
    [HttpGet]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<List<Task>>> GetTasksAsync(string boardId)
        => Ok(await _taskRepository.GetTasksAsync(boardId));

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Task>> CreateTaskAsync([FromBody] Task task)
        => Ok(await _taskRepository.CreateTaskAsync(task));

    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Task>> UpdateTaskAsync([FromBody] Task task)
        => Ok(await _taskRepository.UpdateTaskAsync(task));

    [Route("{id}")]
    [HttpDelete]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<bool>> DeleteTaskAsync(string id)
        => Ok(await _taskRepository.DeleteTaskAsync(id));
}   