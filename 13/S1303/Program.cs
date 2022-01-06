using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

var originalPayload = Guid.NewGuid().ToString();
var protectedPayload = Encrypt("foo", originalPayload, TimeSpan.FromSeconds(5));

var unprotectedPayload = Decrypt("foo", protectedPayload);
Debug.Assert(originalPayload == unprotectedPayload);

await Task.Delay(5000);
Decrypt("foo", protectedPayload);


static string Encrypt(string purpose, string originalPayload, TimeSpan timeout)
    => GetDataProtector(purpose).Protect(originalPayload, DateTimeOffset.UtcNow.Add(timeout));
static string Decrypt(string purpose, string protectedPayload)
    => GetDataProtector(purpose).Unprotect(protectedPayload, out _);

static ITimeLimitedDataProtector GetDataProtector(string purpose)
{
    var services = new ServiceCollection();
    services.AddDataProtection();
    return services
        .BuildServiceProvider()
        .GetDataProtector(purpose)
        .ToTimeLimitedDataProtector();
}
