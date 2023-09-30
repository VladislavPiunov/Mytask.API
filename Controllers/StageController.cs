using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mytask.API.Model;

namespace Mytask.API.Controllers;

[ApiController]
[Route("api/stage")]
public class StageController : ControllerBase
{
    private readonly IBoardRepository _boardRepository;
    private readonly IStageRepository _stageRepository;

    public StageController(
        IBoardRepository boardRepository,
        IStageRepository stageRepository)
    {
        _boardRepository = boardRepository;
        _stageRepository = stageRepository;
    }

    [Route("{boardId}")]
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<List<Stage>>> GetStagesAsync(string boardId)
    {
        var boardStages = _boardRepository.GetBoardByIdAsync(boardId).Result.Stages;

        return Ok(await _stageRepository.GetStagesAsync(boardStages));
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Stage>> CreateStageAsync([FromBody] Stage stage)
        => Ok(await _stageRepository.CreateStageAsync(stage));

    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Stage>> UpdateStageAsync([FromBody] Stage stage)
        => Ok(await _stageRepository.UpdateStageAsync(stage));

    [HttpDelete]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<bool>> DeleteStageAsync(string id)
        => Ok(await _stageRepository.DeleteStageAsync(id));
    
}   