using Microsoft.Extensions.DiagnosticAdapter;
using System.Data;

namespace App
{
    public class DatabaseSourceCollector
    {
        [DiagnosticName("CommandExecution")]
        public void OnCommandExecute(CommandType commandType, string commandText)
        {
            Console.WriteLine($"Event Name: CommandExecution");
            Console.WriteLine($"CommandType: {commandType}");
            Console.WriteLine($"CommandText: {commandText}");
        }
    }
}