using MongoDB.Driver;
using Mytask.API.Model;

namespace Mytask.API.Repositories;

public class StageRepository: IStageRepository
{
    private readonly ILogger<StageRepository> _logger;
    private readonly MongoClient _mongoClient;
    private readonly IMongoDatabase _database;

    public StageRepository(ILogger<StageRepository> logger, MongoClient client)
    {
        _logger = logger;
        _mongoClient = client;
        _database = client.GetDatabase("mytask");
    }
    
    public async Task<List<Stage>> GetStagesAsync(List<string> boardStages)
        => await _database.GetCollection<Stage>("stages")
            .Find(s => boardStages.Contains(s.Id)).ToListAsync();

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
}