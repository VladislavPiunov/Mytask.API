using MongoDB.Driver;
using Mytask.API.Model;
using Task = Mytask.API.Model.Task;

namespace Mytask.API.Repositories;

public class BoardRepository: IBoardRepository
{
    private readonly ILogger<BoardRepository> _logger;
    private readonly MongoClient _mongoClient;
    private readonly IMongoDatabase _database;

    public BoardRepository(ILogger<BoardRepository> logger, MongoClient client)
    {
        _logger = logger;
        _mongoClient = client;
        _database = client.GetDatabase("mytask");
    }

    public async Task<List<Board>> GetBoardsAsync(string ownerId)
        => await _database.GetCollection<Board>("boards")
            .Find(b => b.OwnerId == ownerId || b.Users.Contains(ownerId)).ToListAsync();

    public async Task<Board> GetBoardByIdAsync(string boardId)
        => await _database.GetCollection<Board>("boards").Find(b => b.Id == boardId).SingleOrDefaultAsync();
    
    public async Task<Board> CreateBoardAsync(Board board)
    {
        try
        {
            Stage toDo = new Stage("To Do", "#3399FF");
            await _database.GetCollection<Stage>("stages").InsertOneAsync(toDo);
            
            Stage inProgress = new Stage("In progress", "#FFFF33");
            await _database.GetCollection<Stage>("stages").InsertOneAsync(inProgress);
            
            Stage done = new Stage("Done", "#99FF33");
            await _database.GetCollection<Stage>("stages").InsertOneAsync(done);

            board.Stages = new List<string> { toDo.Id, inProgress.Id, done.Id };
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

        await _database.GetCollection<Model.Task>("tasks")
            .DeleteManyAsync(t => t.BoardId == deleted.Id);
        await _database.GetCollection<Stage>("stages")
            .DeleteManyAsync(s => deleted.Stages.Contains(s.Id));   
        
        _logger.LogInformation("Board deleted successfully.");

        return true;
    }
}