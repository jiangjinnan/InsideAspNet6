using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

var originalPayload = Guid.NewGuid().ToString();
var protectedPayload = Encrypt("foo", originalPayload);
var unprotectedPayload = Decrypt("bar", protectedPayload);
Debug.Assert(originalPayload == unprotectedPayload);

static string Encrypt(string purpose, string originalPayload)
    => GetDataProtector(purpose).Protect(originalPayload);
static string Decrypt(string purpose, string protectedPayload)
    => GetDataProtector(purpose).Unprotect(protectedPayload);

static IDataProtector GetDataProtector(string purpose)
{
    var services = new ServiceCollection();
    services.AddDataProtection();
    return services
        .BuildServiceProvider()
        .GetRequiredService<IDataProtectionProvider>()
        .CreateProtector(purpose);
}
