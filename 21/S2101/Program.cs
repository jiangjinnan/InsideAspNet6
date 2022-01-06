var app = WebApplication.Create();
app.UseDeveloperExceptionPage();
app.MapGet("{foo}/{bar}",void () => throw new InvalidOperationException("Manually thrown exception"));

app.Run();
