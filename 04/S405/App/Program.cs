using App;
using HttpFileSystem;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

var baseAddress = "http://localhost:5000/files/dir1";
var fileSystem = new ServiceCollection()
    .AddSingleton<IFileProvider>(new HttpFileProvider(baseAddress))
    .AddSingleton<IFileSystem, FileSystem>()
    .BuildServiceProvider()
    .GetRequiredService<IFileSystem>();
fileSystem.ShowStructure(Print);
Console.Read();

static void Print(int layer, string name) => Console.WriteLine($"{new string(' ', layer * 4)}{name}");



