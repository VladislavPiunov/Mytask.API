using MongoDB.Bson;

namespace mytask.api.Model;

public interface IMytaskRepository
{
    Task<List<Board>> GetBoardsAsync(string ownerId);
    Task<Board> CreateBoardAsync(Board board);
    Task<Board> UpdateBoardAsync(Board board);
    Task<bool> DeleteBoardAsync(string id);
    
    Task<List<Stage>> GetStagesAsync(string boardId);
    Task<Stage> CreateStageAsync(Stage stage);
    Task<Stage> UpdateStageAsync(Stage stage);
    Task<bool> DeleteStageAsync(string id);
    
    Task<List<Task>> GetTasksAsync(string boardId);
    Task<Task> CreateTaskAsync(Task task);
    Task<Task> UpdateTaskAsync(Task task);
    Task<bool> DeleteTaskAsync(string id);
}