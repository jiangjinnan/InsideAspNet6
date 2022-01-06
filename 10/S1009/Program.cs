using System.Buffers;
using System.Text;

using var fs = new FileStream("test.txt", FileMode.Open);
var length = (int)fs.Length;
var bytes = ArrayPool<byte>.Shared.Rent(length);
try
{
    await fs.ReadAsync(bytes, 0, length);
    Console.WriteLine(Encoding.Default.GetString(bytes, 0, length));
}
finally
{
    ArrayPool<byte>.Shared.Return(bytes);
}
