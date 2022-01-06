var app = WebApplication.Create(args);
var environment = app.Environment;

Console.WriteLine($"ApplicationName:{environment.ApplicationName}");
Console.WriteLine($"ContentRootPath:{environment.ContentRootPath}");
Console.WriteLine($"WebRootPath:{environment.WebRootPath}");
Console.WriteLine($"EnvironmentName:{environment.EnvironmentName}");
