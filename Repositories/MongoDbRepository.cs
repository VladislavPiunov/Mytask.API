using MongoDB.Driver;
using mytask.api.Model;

namespace mytask.api.Repositories;

public class MongoDbRepository : IMytaskRepository
{
    private readonly ILogger<MongoDbRepository> _logger;
    private readonly MongoClient _mongoClient;
    private readonly IMongoDatabase _database;

    public MongoDbRepository(ILogger<MongoDbRepository> logger, MongoClient client)
    {
        _logger = logger;
        _mongoClient = client;
        _database = client.GetDatabase("mytask");
    }

    public async Task<List<Board>> GetBoardsAsync(string ownerId)
        => await _database.GetCollection<Board>("boards")
            .Find(b => b.OwnerId == ownerId || b.Users.Contains(ownerId)).ToListAsync();

    public async Task<Board> CreateBoardAsync(Board board)
    {
        try
        {
            await _database.GetCollection<Board>("boards").InsertOneAsync(board);
            _logger.LogInformation("Board created successfully.");
        }
        catch (Exception e)
        {
            _logger.LogInformation("Problem occur creating the board. Error: " + e);
            return null;
        }

        return board;
    }

    public async Task<Board> UpdateBoardAsync(Board board)
    {
        var updated = await _database.GetCollection<Board>("boards")
            .FindOneAndReplaceAsync(b => b.Id == board.Id, board, new() { ReturnDocument = ReturnDocument.After });
        if (updated == null)
        {
            _logger.LogInformation("Board not found.");
            return null;
        }

        _logger.LogInformation("Board updated successfully.");

        return board;
    }

    public async Task<bool> DeleteBoardAsync(string id)
    {
        var deleted = await _database.GetCollection<Board>("boards")
            .FindOneAndDeleteAsync(b => b.Id == id);
        if (deleted == null)
        {
            _logger.LogInformation("Board not found.");
            return false;
        }

        _logger.LogInformation("Board deleted successfully.");

        return true;
    }

    public async Task<List<Stage>> GetStagesAsync(string boardId)
        => await _database.GetCollection<Stage>("stages")
            .Find(s => s.BoardId == boardId).Sort("{ Order : 1 }").ToListAsync();

    public async Task<Stage> CreateStageAsync(Stage stage)
    {
        try
        {
            await _database.GetCollection<Stage>("stages").InsertOneAsync(stage);
            _logger.LogInformation("Stage created successfully.");
        }
        catch (Exception e)
        {
            _logger.LogInformation("Problem occur creating the stage. Error: " + e);
            return null;
        }

        return stage;
    }

    public async Task<Stage> UpdateStageAsync(Stage stage)
    {
        var updated = await _database.GetCollection<Stage>("stages")
            .FindOneAndReplaceAsync(s => s.Id == stage.Id, stage, new() { ReturnDocument = ReturnDocument.After });
        if (updated == null)
        {
            _logger.LogInformation("Stage not found.");
            return null;
        }

        _logger.LogInformation("Stage updated successfully.");

        return stage;
    }

    public async Task<bool> DeleteStageAsync(string id)
    {
        var deleted = await _database.GetCollection<Stage>("stages")
            .FindOneAndDeleteAsync(s => s.Id == id);
        if (deleted == null)
        {
            _logger.LogInformation("Stage not found.");
            return false;
        }

        _logger.LogInformation("Stage deleted successfully.");

        return true;
    }

    public async Task<List<Model.Task>> GetTasksAsync(string boardId)
        => await _database.GetCollection<Model.Task>("tasks")
            .Find(s => s.BoardId == boardId).ToListAsync();

    public async Task<Model.Task> CreateTaskAsync(Model.Task task)
    {
        try
        {
            await _database.GetCollection<Model.Task>("tasks").InsertOneAsync(task);
            _logger.LogInformation("Task created successfully.");
        }
        catch (Exception e)
        {
            _logger.LogInformation("Problem occur creating the task. Error: " + e);
            return null;
        }

        return task;
    }

    public async Task<Model.Task> UpdateTaskAsync(Model.Task task)
    {
        var updated = await _database.GetCollection<Model.Task>("tasks")
            .FindOneAndReplaceAsync(s => s.Id == task.Id, task, new() { ReturnDocument = ReturnDocument.After });
        if (updated == null)
        {
            _logger.LogInformation("Task not found.");
            return null;
        }

        _logger.LogInformation("Task updated successfully.");

        return task;
    }

    public async Task<bool> DeleteTaskAsync(string id)
    {
        var deleted = await _database.GetCollection<Model.Task>("tasks")
            .FindOneAndDeleteAsync(s => s.Id == id);
        if (deleted == null)
        {
            _logger.LogInformation("Task not found.");
            return false;
        }

        _logger.LogInformation("Task deleted successfully.");

        return true;
    }
}