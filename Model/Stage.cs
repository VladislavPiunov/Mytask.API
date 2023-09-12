using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace mytask.api.Model;

public class Stage
{
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; private set; } = "";
    [BsonRepresentation(BsonType.ObjectId)]
    public string BoardId { get; private set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public int Order { get; set; }

    public Stage(string boardId, string name, string color, int order)
    {
        BoardId = boardId;
        Name = name;
        Color = color;
        Order = order;
    }
}