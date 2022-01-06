using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddInMemoryCollection(new Dictionary<string, string>
{
    ["foo"] = "123",
    ["bar"] = "456"
});
var app = builder.Build();
Debug.Assert(app.Configuration["foo"] == "123");
Debug.Assert(app.Configuration["bar"] == "456");
