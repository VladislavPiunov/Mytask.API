using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mytask.API.Model;
using Mytask.API.Services;

namespace Mytask.API.Controllers;

[ApiController]
[Route("api/board")]
public class BoardController : ControllerBase
{
    private readonly IBoardRepository _boardRepository;
    private readonly IIdentityService _identityService;

    public BoardController(
        IBoardRepository boardRepository,
        IIdentityService identityService)
    {
        _boardRepository = boardRepository;
        _identityService = identityService;
    }

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

    [HttpPost]
    [Authorize]
    public async Task<ActionResult<Board>> CreateBoardAsync([FromBody] Board board)
    {
        board.OwnerId = _identityService.GetUserIdentity();
        
        var newBoard = await _boardRepository.CreateBoardAsync(board);

        return Ok(newBoard);
    }

    [HttpPut]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<Board>> UpdateBoardAsync([FromBody] Board board)
     => Ok(await _boardRepository.UpdateBoardAsync(board));


    [Route("{id}")]
    [HttpDelete]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<bool>> DeleteBoardAsync(string id) 
        => Ok(await _boardRepository.DeleteBoardAsync(id));
}   