using GameStore.Api.Data;
using GameStore.Api.Endpoints;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


var connString = builder.Configuration.GetConnectionString("GameStore"); // get connstring value from appsettings.json through a depency injection

builder.Services.AddSqlite<GameStoreContext>(connString); // add  connectionstring to entityframework

// --- don't store credentials (db pw=...) in appsettings.json when in production, 
//---  instead use Iconfiguration with User Secrets as interface between appsettings and rest api

// resources: install https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Sqlite/8.0.2 
//            install https://www.nuget.org/packages/dotnet-ef/8.0.2 (tools for passing EF commands)
//            install https://www.nuget.org/packages/Microsoft.EntityFrameworkCore.Design/8.0.2 (for building a sqlite database through entityframework with migrations )

WebApplication app = builder.Build();

app.MapGamesEndpoints();
app.MapGenresEndpoints();

// call asynchronous method to build the db
await app.MigrateDbAsync();

app.Run();
