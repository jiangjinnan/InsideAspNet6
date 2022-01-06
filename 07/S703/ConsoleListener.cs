using System.Diagnostics;

namespace App
{
    public class ConsoleListener : TraceListener
    {
        public override void Write(string? message) => Console.Write(message);
        public override void WriteLine(string? message) => Console.WriteLine(message);
    }
}