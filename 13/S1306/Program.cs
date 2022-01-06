using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;

var originalPayload = Guid.NewGuid().ToString();
var dataProtectionProvider = CreateEphemeralDataProtectionProvider();
var protector = dataProtectionProvider.CreateProtector("foobar");
var protectedPayload = protector.Protect(originalPayload);

protector = dataProtectionProvider.CreateProtector("foobar");
Debug.Assert(originalPayload == protector.Unprotect(protectedPayload));

protector = CreateEphemeralDataProtectionProvider().CreateProtector("foobar");
protector.Unprotect(protectedPayload);

static IDataProtectionProvider CreateEphemeralDataProtectionProvider()
{
    var services = new ServiceCollection();
    services.AddDataProtection().UseEphemeralDataProtectionProvider();
    return services
        .BuildServiceProvider()
        .GetRequiredService<IDataProtectionProvider>();
}
