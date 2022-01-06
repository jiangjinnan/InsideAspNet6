using App2;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddActors(options => options.Actors.RegisterActor<Accumulator>());
var app = builder.Build();
app.MapActorsHandlers();
app.Run("http://localhost:9999");
