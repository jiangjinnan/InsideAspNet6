using App;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Diagnostics;
using System.Reflection;
using System.Text;

var assembly = Assembly.GetEntryAssembly()!;
var content = await new ServiceCollection()
    .AddSingleton<IFileProvider>(new EmbeddedFileProvider(assembly))
    .AddSingleton<IFileSystem, FileSystem>()
    .BuildServiceProvider()
    .GetRequiredService<IFileSystem>()
    .ReadAllTextAsync("data.txt");

var stream = assembly.GetManifestResourceStream($"{assembly.GetName().Name}.data.txt");
var buffer = new byte[stream!.Length];
stream.Read(buffer, 0, buffer.Length);

Debug.Assert(content == Encoding.Default.GetString(buffer));


