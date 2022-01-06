using System.Diagnostics;
using System.Net;

DiagnosticListener.AllListeners.Subscribe(listener =>
{
    if (listener.Name == "Web")
    {
        listener.Subscribe(eventData =>
        {
            if (eventData.Key == "ReceiveRequest" && eventData.Value != null)
            {
                dynamic payload = eventData.Value;
                var request = (HttpRequestMessage)(payload.Request);
                var timestamp = (long)payload.Timestamp;
                Console.WriteLine(@$"Receive request. Url: {request.RequestUri}; Timstamp:{ timestamp}");
            }
            if (eventData.Key == "SendReply" && eventData.Value != null)
            {
                dynamic payload = eventData.Value;
                var response = (HttpResponseMessage)(payload.Response);
                var elaped = (TimeSpan)payload.Elaped;
                Console.WriteLine($"Send reply. Status code: {response.StatusCode}; Elaped: {elaped}");
            }
        });
    }
});

var source = new DiagnosticListener("Web");
var stopwatch = Stopwatch.StartNew();
if (source.IsEnabled("ReceiveRequest"))
{
    var request = new HttpRequestMessage(HttpMethod.Get, "https://www.artech.top");
    source.Write("ReceiveRequest", new
    {
        Request = request,
        Timestamp = Stopwatch.GetTimestamp()
    });
}
await Task.Delay(100);
if (source.IsEnabled("SendReply"))
{
    var response = new HttpResponseMessage(HttpStatusCode.OK);
    source.Write("SendReply", new
    {
        Response = response,
        Elaped = stopwatch.Elapsed
    });
}
