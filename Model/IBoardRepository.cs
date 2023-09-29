namespace Mytask.API.Model;

public interface IBoardRepository
{
    Task<List<Board>> GetBoardsAsync(string ownerId);
    Task<Board> GetBoardByIdAsync(string boardId);
    Task<Board> CreateBoardAsync(Board board);
    Task<Board> UpdateBoardAsync(Board board);
    Task<bool> DeleteBoardAsync(string id);
}