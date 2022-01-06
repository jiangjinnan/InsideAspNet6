using App;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var configuration = new ConfigurationManager();
configuration.AddJsonFile(
    path: "profile.json", 
    optional: false, 
    reloadOnChange: true);

new ServiceCollection()
    .AddOptions()
    .Configure<Profile>(configuration)
    .BuildServiceProvider()
    .GetRequiredService<IOptionsMonitor<Profile>>()
    .OnChange(profile =>
    {
        Console.WriteLine($"Gender: {profile.Gender}");
        Console.WriteLine($"Age: {profile.Age}");
        Console.WriteLine($"Email Address: {profile.ContactInfo?.EmailAddress}");
        Console.WriteLine($"Phone No: {profile.ContactInfo?.PhoneNo}\n");
    });
Console.Read();