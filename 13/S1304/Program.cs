using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.DataProtection.KeyManagement.Internal;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddDataProtection();
var sericeProvider = services.BuildServiceProvider();
var protector = sericeProvider.GetDataProtector("foobar");
var originalPayload = Guid.NewGuid().ToString();
var protectedPayload = protector.Protect(originalPayload);

var keyRingProvider = sericeProvider.GetRequiredService<IKeyRingProvider>();
var KeyRing = keyRingProvider.GetCurrentKeyRing();
var keyManager = sericeProvider.GetRequiredService<IKeyManager>();
keyManager.RevokeKey(KeyRing.DefaultKeyId);
protector.Unprotect(protectedPayload);
