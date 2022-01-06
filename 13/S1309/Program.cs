
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

var key = keyManager.GetAllKeys().First();
keyManager.RevokeKey(key.KeyId);
keyManager.RevokeAllKeys(DateTimeOffset.Now, "Revocation Test");
