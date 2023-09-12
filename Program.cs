using System.Security.Claims;
using Keycloak.AuthServices.Authentication;
using MongoDB.Bson;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
// Add KeyCloak
builder.Services.AddKeycloakAuthentication(builder.Configuration);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Add MongoDb
builder.Services.AddHealthChecks().AddMongoDb(builder.Configuration.GetRequiredConnectionString("Mongo"));
builder.Services.AddSingleton(new MongoClient(builder.Configuration.GetRequiredConnectionString("Mongo")));


var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
    app.UseSwagger();
    app.UseSwaggerUI();
// }

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", async (MongoClient client) =>     // получаем MongoClient через DI
{
    var db = client.GetDatabase("mytask");    // обращаемся к базе данных
    var collection = db.GetCollection<BsonDocument>("users"); // получаем коллекцию users
    // для теста добавляем начальные данные, если коллекция пуста
    if (await collection.CountDocumentsAsync("{}") == 0)
    {
        await collection.InsertManyAsync(new List<BsonDocument>
        {
            new BsonDocument{ { "Name", "Tom" },{"Age", 22}},
            new BsonDocument{ { "Name", "Bob" },{"Age", 42}}
        });
    }
    var users =  await collection.Find("{}").ToListAsync();
    db.DropCollection("users");
    return users.ToJson();  // отправляем клиенту все документы из коллекции
});

app.Run();