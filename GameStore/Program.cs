using GameStore.EndPoints;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGameEndPoints();

app.Run();