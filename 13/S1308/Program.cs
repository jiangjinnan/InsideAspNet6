using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.DependencyInjection;

var directory = "c:\\keys";
var services = new ServiceCollection();
services
    .AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(directory));
var keyManager = services
    .BuildServiceProvider()
    .GetRequiredService<IKeyManager>();

var key1 = keyManager.CreateNewKey(DateTimeOffset.Now, DateTimeOffset.Now.AddDays(1));
var key2 = keyManager.CreateNewKey(DateTimeOffset.Now ,DateTimeOffset.Now.AddDays(2));
var key3 = keyManager.CreateNewKey(DateTimeOffset.Now, DateTimeOffset.Now.AddDays(3));

Console.WriteLine(key1.KeyId);
Console.WriteLine(key2.KeyId);
Console.WriteLine(key3.KeyId);
