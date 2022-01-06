using HttpMachine;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http.Features;
using System.Buffers;
using System.IO.Pipelines;
using System.Text;

namespace App
{
public class HostedApplication<TContext> : ConnectionHandler where TContext : notnull
{
    private readonly IHttpApplication<TContext> _application;
    public HostedApplication(IHttpApplication<TContext> application) => _application = application;
    public override async Task OnConnectedAsync(ConnectionContext connection)
    {
        var reader = connection!.Transport.Input;
        while (true)
        {
            var result = await reader.ReadAsync();
            using (var body = new MemoryStream())
            {
                var (features, request, response) = CreateFeatures(result, body);
                var closeConnection = request.Headers.TryGetValue("Connection", out var vallue) && vallue == "Close";
                reader.AdvanceTo(result.Buffer.End);

                var context = _application.CreateContext(features);
                Exception? exception = null;
                try
                {
                    await _application.ProcessRequestAsync(context);
                    await ApplyResponseAsync(connection, response, body);
                }
                catch (Exception ex)
                {
                    exception = ex;
                }
                finally
                {
                    _application.DisposeContext(context, exception);
                }
                if (closeConnection)
                {
                    await connection.DisposeAsync();
                    return;
                }
            }
            if (result.IsCompleted)
            {
                break;
            }
        }

        static (IFeatureCollection, IHttpRequestFeature, IHttpResponseFeature) CreateFeatures(ReadResult result, Stream body)
        {
            var handler = new HttpParserHandler();
            var parserHandler = new HttpParser(handler);
            var length = (int)result.Buffer.Length;
            var array = ArrayPool<byte>.Shared.Rent(length);
            try
            {
                result.Buffer.CopyTo(array);
                parserHandler.Execute(new ArraySegment<byte>(array, 0, length));
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(array);
            }
            var bodyFeature = new StreamBodyFeature(body);

            var features = new FeatureCollection();
            var responseFeature = new HttpResponseFeature();
            features.Set<IHttpRequestFeature>(handler.Request);
            features.Set<IHttpResponseFeature>(responseFeature);
            features.Set<IHttpResponseBodyFeature>(bodyFeature);

            return (features, handler.Request, responseFeature);
        }
        static async Task ApplyResponseAsync(ConnectionContext connection, IHttpResponseFeature response, Stream body)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"HTTP/1.1 {response.StatusCode} {response.ReasonPhrase}");
            foreach (var kv in response.Headers)
            {
                builder.AppendLine($"{kv.Key}: {kv.Value}");
            }
            builder.AppendLine($"Content-Length: {body.Length}");
            builder.AppendLine();
            var bytes = Encoding.UTF8.GetBytes(builder.ToString());

            var writer = connection.Transport.Output;
            await writer.WriteAsync(bytes);
            body.Position = 0;
            await body.CopyToAsync(writer);
        }
    }
}
}
