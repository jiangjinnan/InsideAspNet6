using App;
using HttpFileSystem;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System.Diagnostics;

var baseAddress = "http://localhost:5000/files/dir1";
var fileSystem = new ServiceCollection()
    .AddSingleton<IFileProvider>(new HttpFileProvider(baseAddress))
    .AddSingleton<IFileSystem, FileSystem>()
    .BuildServiceProvider()
    .GetRequiredService<IFileSystem>();

var content1 = await fileSystem.ReadAllTextAsync("foobar/foo.txt");
var content2 = await File.ReadAllTextAsync(@"c:\test\dir1\foobar\foo.txt");
Debug.Assert(content1 == content2);