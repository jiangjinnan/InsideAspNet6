using App;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

var configuration = new ConfigurationManager();
configuration.AddJsonFile("profile.json");
var profile = new ServiceCollection()
    .AddOptions()
    .Configure<Profile>(configuration)
    .BuildServiceProvider()
    .GetRequiredService<IOptions<Profile>>().Value;
Console.WriteLine($"Gender: {profile.Gender}");
Console.WriteLine($"Age: {profile.Age}");
Console.WriteLine($"Email Address: {profile.ContactInfo?.EmailAddress}");
Console.WriteLine($"Phone No: {profile.ContactInfo?.PhoneNo}");
