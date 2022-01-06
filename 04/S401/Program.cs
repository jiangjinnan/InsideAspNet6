using App;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;

static void Print(int layer, string name) => Console.WriteLine($"{new string(' ', layer * 4)}{name}");
new ServiceCollection()
    .AddSingleton<IFileProvider>(new PhysicalFileProvider(@"c:\test"))
    .AddSingleton<IFileSystem, FileSystem>()
    .BuildServiceProvider()
    .GetRequiredService<IFileSystem>()
    .ShowStructure(Print);
