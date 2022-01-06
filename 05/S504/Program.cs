using App;
using Microsoft.Extensions.Configuration;

var options = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build()
    .GetSection("format")
    .Get<FormatOptions>();

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