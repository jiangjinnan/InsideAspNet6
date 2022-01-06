using App;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Memory;

var source = new Dictionary<string, string>
{
    ["longDatePattern"] = "dddd, MMMM d, yyyy",
    ["longTimePattern"] = "h:mm:ss tt",
    ["shortDatePattern"] = "M/d/yyyy",
    ["shortTimePattern"] = "h:mm tt"
};

var config = new ConfigurationBuilder()
    .Add(new MemoryConfigurationSource { InitialData = source })
    .Build();

var options = new DateTimeFormatOptions(config);
Console.WriteLine($"LongDatePattern: {options.LongDatePattern}");
Console.WriteLine($"LongTimePattern: {options.LongTimePattern}");
Console.WriteLine($"ShortDatePattern: {options.ShortDatePattern}");
Console.WriteLine($"ShortTimePattern: {options.ShortTimePattern}");