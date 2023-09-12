using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mytask.api.Model;

public class Task
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; private set; } = "";
    public string Name { get; set; }
    [BsonRepresentation(BsonType.ObjectId)]
    public string BoardId { get; private set; }
    public string Description { get; set; } = "";
    public DateTime? Deadline { get; set; } = null;
    public string Executor { get; set; } = "";

    public Task(string name, string boardId)
    {
        Name = name;
        BoardId = boardId;
    }
}