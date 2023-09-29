using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mytask.API.Model;

public class Board
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = "";

    public string Name { get; set; } = "Myboard";
    
    public string OwnerId { get; set; }

    public List<string> Stages { get; set; } = new();
    public List<string> Users { get; set; } = new();
    
    public Board(string ownerId)
    {
        OwnerId = ownerId;
    }
}