using App;
using System.Diagnostics;

var source = new TraceSource("Foobar", SourceLevels.Warning);
source.Listeners.Add(new ConsoleListener());
var eventTypes = (TraceEventType[])Enum.GetValues(typeof(TraceEventType));
var eventId = 1;
Array.ForEach(eventTypes, it => source.TraceEvent(it, eventId++,$"This is a {it} message."));