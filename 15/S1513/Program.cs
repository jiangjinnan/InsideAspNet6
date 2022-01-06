using System.Diagnostics;

Environment.SetEnvironmentVariable("ASPNETCORE_FOO", "123");
Environment.SetEnvironmentVariable("ASPNETCORE_BAR", "456");

var builder = WebApplication.CreateBuilder(args);
IConfiguration configuration = builder.Configuration;
Debug.Assert(configuration["foo"] == "123");
Debug.Assert(configuration["bar"] == "456");

var app = builder.Build();
configuration = app.Configuration;
Debug.Assert(configuration["foo"] == "123");
Debug.Assert(configuration["bar"] == "456");
