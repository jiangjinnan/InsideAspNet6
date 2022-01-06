using Microsoft.Extensions.Configuration;

try
{
    var mapping = new Dictionary<string, string>
    {
        ["-a"] = "architecture",
        ["-arch"] = "architecture"
    };
    var configuration = new ConfigurationBuilder()
        .AddCommandLine(args, mapping)
        .Build();
    Console.WriteLine($"Architecture: {configuration["architecture"]}");
}
catch (Exception ex)
{
    Console.WriteLine($"Error: {ex.Message}");
}
