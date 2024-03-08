using GameStore.Data;
using GameStore.EndPoints;

var builder = WebApplication.CreateBuilder(args);

var connString = builder.Configuration.GetConnectionString("GameStore");
builder.Services.AddSqlite<GameStoreContext>(connString);

var app = builder.Build();

app.MapGameEndPoints();

app.MigrateDB();

app.Run();