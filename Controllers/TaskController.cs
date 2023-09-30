using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mytask.API.Model;
using Task = Mytask.API.Model.Task;

namespace Mytask.API.Controllers;

[ApiController]
[Route("api")]
public class TaskController : ControllerBase
{
    private readonly ITaskRepository _taskRepository;

    public TaskController(
        ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    [Route("task/{boardId}")]
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<Task>>> GetTasksAsync(string boardId)
        => await _taskRepository.GetTasksAsync(boardId);

    [Route("task")]
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Task>> CreateTaskAsync([FromBody] Task task)
        => await _taskRepository.CreateTaskAsync(task);

    [Route("task")]
    [HttpPut]
    [Authorize]
    public async Task<ActionResult<Task>> UpdateTaskAsync([FromBody] Task task)
        => await _taskRepository.UpdateTaskAsync(task);

    [Route("task/{id}")]
    [HttpDelete]
    [Authorize]
    public async Task<ActionResult<bool>> DeleteTaskAsync(string id)
        => await _taskRepository.DeleteTaskAsync(id);
}   