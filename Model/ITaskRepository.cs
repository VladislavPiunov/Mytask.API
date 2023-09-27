using MongoDB.Bson;

namespace Mytask.API.Model;

public interface ITaskRepository
{
    Task<List<Task>> GetTasksAsync(string boardId);
    Task<Task> CreateTaskAsync(Task task);
    Task<Task> UpdateTaskAsync(Task task);
    Task<bool> DeleteTaskAsync(string id);
}