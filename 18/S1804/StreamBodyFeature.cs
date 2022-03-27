using Microsoft.AspNetCore.Http.Features;
using System.IO.Pipelines;

namespace App
{
    public class StreamBodyFeature : IHttpResponseBodyFeature
    {
        public Stream Stream { get; }
        public PipeWriter Writer { get; }
        public StreamBodyFeature(Stream stream )
        {
            Stream = stream;
            Writer = PipeWriter.Create(Stream);
        }

        public Task CompleteAsync()=>Task.CompletedTask;
        public void DisableBuffering() { }
        public Task SendFileAsync(string path, long offset, long? count, CancellationToken cancellationToken = default)=> throw new NotImplementedException();
        public Task StartAsync(CancellationToken cancellationToken = default) => Task.CompletedTask;
    }
}
