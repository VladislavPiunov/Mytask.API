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

app.Run();