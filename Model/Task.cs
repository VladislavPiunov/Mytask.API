using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mytask.API.Model;

public class Task
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = "";
    public string Name { get; set; }
    [BsonRepresentation(BsonType.ObjectId)]
    public string BoardId { get; set; }
    public string StageId { get; set; } 
    public string Description { get; set; } = "";
    public DateTime? Deadline { get; set; } = null;
    public string Executor { get; set; } = "";

    public Task(string name, string boardId, string stageId)
    {
        Name = name;
        BoardId = boardId;
        StageId = stageId;
    }
}