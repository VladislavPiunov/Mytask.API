using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mytask.api.Model;

public class Board
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; private set; } = "";
    public string Name { get; set; }
    [BsonRepresentation(BsonType.ObjectId)]
    public string OwnerId { get; private set; }
    public List<string> Users { get; set; } = new();

    public Board(string name, string ownerId, List<string> users)
    {
        Name = name;
        OwnerId = ownerId;
        Users = users;
    }
}