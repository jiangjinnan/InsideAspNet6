using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddDataProtection();
var sericeProvider = services.BuildServiceProvider();
var protector = sericeProvider.GetDataProtector("foobar");
var originalPayload = Guid.NewGuid().ToString();
var protectedPayload = protector.Protect(originalPayload);

var keyManager = sericeProvider.GetRequiredService<IKeyManager>();
keyManager.RevokeAllKeys(revocationDate: DateTimeOffset.UtcNow, reason: "No reason");
protector.Unprotect(protectedPayload);
