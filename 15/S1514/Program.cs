using System.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.UseSetting("foo", "abc");
builder.WebHost.UseSetting("bar", "xyz");

Debug.Assert(builder.WebHost.GetSetting("foo") == "abc");
Debug.Assert(builder.WebHost.GetSetting("bar") == "xyz");

IConfiguration configuration = builder.Configuration;
Debug.Assert(configuration["foo"] == "abc");
Debug.Assert(configuration["bar"] == "xyz");

var app = builder.Build();
configuration = app.Configuration;
Debug.Assert(configuration["foo"] == "abc");
Debug.Assert(configuration["bar"] == "xyz");
