using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Mytask.API.Model;

public class Stage
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = "";
    [BsonRepresentation(BsonType.ObjectId)]
    public string Name { get; set; }
    public string Color { get; set; }

    public Stage(string name, string color)
    {
        Name = name;
        Color = color;
    }
}