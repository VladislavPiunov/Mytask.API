using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mytask.API.Model;
using Mytask.API.Services;
using Task = Mytask.API.Model.Task;

namespace Mytask.API.Controllers;

[ApiController]
[Route("api")]
public class MytaskController : ControllerBase
{
    private readonly IBoardRepository _boardRepository;
    private readonly IStageRepository _stageRepository;
    private readonly ITaskRepository _taskRepository;
    private readonly IIdentityService _identityService;

    public MytaskController(
        IBoardRepository boardRepository,
        IStageRepository stageRepository,
        ITaskRepository taskRepository,
        IIdentityService identityService)
    {
        _boardRepository = boardRepository;
        _stageRepository = stageRepository;
        _taskRepository = taskRepository;
        _identityService = identityService;
    }

    [Route("board")]
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<Board>>> GetBoardsAsync()
    {
        var userId = _identityService.GetUserIdentity();
        var boards = await _boardRepository.GetBoardsAsync(userId);

        if (boards.Count == 0)
        {
            var newBoard = await _boardRepository.CreateBoardAsync(new Board(userId));
            boards.Add(newBoard);
        }

        return Ok(boards);
    }

    [Route("board")]
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Board>> CreateBoardAsync([FromBody] Board board)
    {
        board.OwnerId = _identityService.GetUserIdentity();
        
        var newBoard = await _boardRepository.CreateBoardAsync(board);

        return Ok(newBoard);
    }

    [Route("board")]
    [HttpPut]
    [Authorize]
    public async Task<ActionResult<Board>> UpdateBoardAsync([FromBody] Board board)
     => await _boardRepository.UpdateBoardAsync(board);


    [Route("board/{id}")]
    [HttpDelete]
    [Authorize]
    public async Task<ActionResult<bool>> DeleteBoardAsync(string id) 
        => await _boardRepository.DeleteBoardAsync(id);

    [Route("stage/{boardId}")]
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<Stage>>> GetStagesAsync(string boardId)
    {
        var boardStages = _boardRepository.GetBoardByIdAsync(boardId).Result.Stages;

        return await _stageRepository.GetStagesAsync(boardStages);
    }

    [Route("stage")]
    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Stage>> CreateStageAsync([FromBody] Stage stage)
        => await _stageRepository.CreateStageAsync(stage);

    [Route("stage")]
    [HttpPut]
    [Authorize]
    public async Task<ActionResult<Stage>> UpdateStageAsync([FromBody] Stage stage)
        => await _stageRepository.UpdateStageAsync(stage);

    [Route("stage/{id}")]
    [HttpDelete]
    [Authorize]
    public async Task<ActionResult<bool>> DeleteStageAsync(string id)
        => await _stageRepository.DeleteStageAsync(id);

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