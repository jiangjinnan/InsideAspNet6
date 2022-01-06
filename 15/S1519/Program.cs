var options = new WebApplicationOptions
{
    Args = args,
    ApplicationName = "MyApp",
    ContentRootPath = Path.Combine(Directory.GetCurrentDirectory(), "contents"),
    EnvironmentName = "staging"
};
var app = WebApplication.CreateBuilder(options).Build();
var environment = app.Environment;
Console.WriteLine($"ApplicationName:{environment.ApplicationName}");
Console.WriteLine($"ContentRootPath:{environment.ContentRootPath}");
Console.WriteLine($"WebRootPath:{environment.WebRootPath}");
Console.WriteLine($"EnvironmentName:{environment.EnvironmentName}");