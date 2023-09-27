using MongoDB.Driver;
using Mytask.API.Model;

namespace Mytask.API.Repositories;

public class TaskRepository : ITaskRepository
{
    private readonly ILogger<TaskRepository> _logger;
    private readonly MongoClient _mongoClient;
    private readonly IMongoDatabase _database;

    public TaskRepository(ILogger<TaskRepository> logger, MongoClient client)
    {
        _logger = logger;
        _mongoClient = client;
        _database = client.GetDatabase("mytask");
    }
    
    public async Task<List<Model.Task>> GetTasksAsync(string boardId)
        => await _database.GetCollection<Model.Task>("tasks")
            .Find(t => t.BoardId == boardId).ToListAsync();

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
            .FindOneAndReplaceAsync(t => t.Id == task.Id, task, new() { ReturnDocument = ReturnDocument.After });
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
            .FindOneAndDeleteAsync(t => t.Id == id);
        if (deleted == null)
        {
            _logger.LogInformation("Task not found.");
            return false;
        }

        _logger.LogInformation("Task deleted successfully.");

        return true;
    }
}