using App;
using System.Diagnostics;
using System.Net;

DiagnosticListener.AllListeners.Subscribe(listener =>
{
    if (listener.Name == "Web")
    {
        listener.SubscribeWithAdapter(new DiagnosticCollector());
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
