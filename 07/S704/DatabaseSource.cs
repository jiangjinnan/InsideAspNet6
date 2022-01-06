using System.Data;
using System.Diagnostics.Tracing;

namespace App
{
    public sealed class DatabaseSource : EventSource
    {
        public static readonly DatabaseSource Instance = new();
        private DatabaseSource() { }

        public void OnCommandExecute(CommandType commandType, string commandText)
        => WriteEvent(1, commandType, commandText);
    }
}