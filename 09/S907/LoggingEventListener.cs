using System.Diagnostics.Tracing;
using System.Text.Json;

namespace App
{
public class LoggingEventListener : EventListener
{
    protected override void OnEventSourceCreated(EventSource eventSource)
    {
        if (eventSource.Name == "System.Threading.Tasks.TplEventSource")
        {
            EnableEvents(eventSource, EventLevel.Informational, (EventKeywords)0x80);
        }

        if (eventSource.Name == "Microsoft-Extensions-Logging")
        {
            EnableEvents(eventSource, EventLevel.LogAlways, (EventKeywords)8);
        }
    }

    protected override void OnEventWritten(EventWrittenEventArgs eventData)
    {
        var payloadNames = eventData.PayloadNames;
        if (payloadNames != null)
        {
            int index;
            var payload = (index = payloadNames.IndexOf("ArgumentsJson")) == -1
                ? null
                : eventData.Payload?[index];
            var relatedActivityId = eventData.RelatedActivityId == default
                ? ""
                : eventData.RelatedActivityId.ToString();

            File.AppendAllLines("log.csv", new string[] { $"{eventData.EventName}, {payload}, {eventData.ActivityId}, {relatedActivityId}" });
        }
    }
}
}