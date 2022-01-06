namespace App
{
public class DelayHttpMessageHanadler : DelegatingHandler
{
    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        await Task.Delay(TimeSpan.FromSeconds(1), cancellationToken);
        var response = await base.SendAsync(request, cancellationToken);
        await Task.Delay(TimeSpan.FromSeconds(2), cancellationToken);
        return response;
    }
}
}