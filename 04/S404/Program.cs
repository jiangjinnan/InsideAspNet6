using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Primitives;
using System.Text;

using var fileProvider = new PhysicalFileProvider(@"c:\test");
string? original = null;
ChangeToken.OnChange(() => fileProvider.Watch("data.txt"), Callback);
while (true)
{
    File.WriteAllText(@"c:\test\data.txt", DateTime.Now.ToString());
    await Task.Delay(5000);
}

async void Callback()
{
    var stream = fileProvider.GetFileInfo("data.txt").CreateReadStream();
    {
        var buffer = new byte[stream.Length];
        await stream.ReadAsync(buffer);
        var current = Encoding.Default.GetString(buffer);
        if (current != original)
        {
            Console.WriteLine(original = current);
        }
    }
}
