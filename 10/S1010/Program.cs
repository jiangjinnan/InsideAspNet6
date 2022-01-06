using System.Buffers;
using System.Text;

using var fs = new FileStream("test.txt", FileMode.Open);
var length = (int)fs.Length;
using (var memoryOwner = MemoryPool<byte>.Shared.Rent(length))
{
    await fs.ReadAsync(memoryOwner.Memory);
    Console.WriteLine(Encoding.Default.GetString(memoryOwner.Memory.Span.Slice(0, length)));
}
