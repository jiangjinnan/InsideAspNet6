using App;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

Environment.SetEnvironmentVariable("TEST_GENDER", "Male");
Environment.SetEnvironmentVariable("TEST_AGE", "18");
Environment.SetEnvironmentVariable("TEST_CONTACTINFO:EMAILADDRESS", "foobar@outlook.com");
Environment.SetEnvironmentVariable("TEST_CONTACTINFO__PHONENO", "123456789");

var profile = new ConfigurationBuilder()
    .AddEnvironmentVariables("TEST_")
    .Build()
    .Get<Profile>();

Debug.Assert(profile.Gender == Gender.Male);
Debug.Assert(profile.Age == 18);
Debug.Assert(profile.ContactInfo.EmailAddress == "foobar@outlook.com");
Debug.Assert(profile.ContactInfo.PhoneNo == "123456789");
