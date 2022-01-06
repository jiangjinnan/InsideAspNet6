using App;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

var source = new Dictionary<string, string>
{
    ["gender"] = "Male",
    ["age"] = "18",
    ["contactInfo:emailAddress"] = "foobar@outlook.com",
    ["contactInfo:phoneNo"] = "123456789"
};

var configuration = new ConfigurationBuilder()
    .AddInMemoryCollection(source)
    .Build();

var profile = configuration.Get<Profile>();
Debug.Assert(profile.Gender == Gender.Male);
Debug.Assert(profile.Age == 18);
Debug.Assert(profile.ContactInfo.EmailAddress == "foobar@outlook.com");
Debug.Assert(profile.ContactInfo.PhoneNo == "123456789");