using System.Data;
using System.Diagnostics.Tracing;

namespace App
{
    [EventSource(Name = "Artech-Data-SqlClient")]
    public sealed class DatabaseSource : EventSource
    {
        public static DatabaseSource Instance { get; } = new();
        private DatabaseSource() { }

        [Event(1, Level = EventLevel.Informational, Keywords = EventKeywords.None,
            Opcode = EventOpcode.Info, Task = Tasks.DA, Tags = Tags.MSSQL, Version = 1,
            Message = "Execute SQL command. Type: {0}, Command Text: {1}")]
        public void OnCommandExecute(CommandType commandType, string commandText)
        {
            if (IsEnabled(EventLevel.Informational, EventKeywords.All, EventChannel.Debug))
            {
                WriteEvent(1, (int)commandType, commandText);
            }
        }
    }

    public class Tasks
    {
        public const EventTask UI = (EventTask)1;
        public const EventTask Business = (EventTask)2;
        public const EventTask DA = (EventTask)3;
    }

    public class Tags
    {
        public const EventTags MSSQL = (EventTags)1;
        public const EventTags Oracle = (EventTags)2;
        public const EventTags Db2 = (EventTags)3;
    }


}
