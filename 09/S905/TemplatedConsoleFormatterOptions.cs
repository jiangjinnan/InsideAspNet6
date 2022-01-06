using Microsoft.Extensions.Logging.Console;

namespace App
{
    public class TemplatedConsoleFormatterOptions : ConsoleFormatterOptions
    {
        public string? Template { get; set; }
    }
}