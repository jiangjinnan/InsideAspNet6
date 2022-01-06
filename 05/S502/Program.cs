using App;
using Microsoft.Extensions.Configuration;

var source = new Dictionary<string, string>
{
    ["format:dateTime:longDatePattern"] = "dddd, MMMM d, yyyy",
    ["format:dateTime:longTimePattern"] = "h:mm:ss tt",
    ["format:dateTime:shortDatePattern"] = "M/d/yyyy",
    ["format:dateTime:shortTimePattern"] = "h:mm tt",

    ["format:currencyDecimal:digits"] = "2",
    ["format:currencyDecimal:symbol"] = "$",
};
var configuration = new ConfigurationBuilder()
        .AddInMemoryCollection(source)
        .Build();

var options = new FormatOptions(configuration.GetSection("Format"));
var dateTime = options.DateTime;
var currencyDecimal = options.CurrencyDecimal;

Console.WriteLine("DateTime:");
Console.WriteLine($"\tLongDatePattern: {dateTime.LongDatePattern}");
Console.WriteLine($"\tLongTimePattern: {dateTime.LongTimePattern}");
Console.WriteLine($"\tShortDatePattern: {dateTime.ShortDatePattern}");
Console.WriteLine($"\tShortTimePattern: {dateTime.ShortTimePattern}");

Console.WriteLine("CurrencyDecimal:");
Console.WriteLine($"\tDigits:{currencyDecimal.Digits}");
Console.WriteLine($"\tSymbol:{currencyDecimal.Symbol}");