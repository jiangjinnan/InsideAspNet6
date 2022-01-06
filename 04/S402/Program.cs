using App;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Diagnostics;

var content = await new ServiceCollection()
    .AddSingleton<IFileProvider>(new PhysicalFileProvider(@"c:\test"))
    .AddSingleton<IFileSystem, FileSystem>()
    .BuildServiceProvider()
    .GetRequiredService<IFileSystem>()
    .ReadAllTextAsync("data.txt");

Debug.Assert(content == File.ReadAllText(@"c:\test\data.txt"));
