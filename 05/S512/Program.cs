using App;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

var source = new Dictionary<string, string>
{
    ["foo:gender"] = "Male",
    ["foo:age"] = "18",
    ["foo:contactInfo:emailAddress"] = "foo@outlook.com",
    ["foo:contactInfo:phoneNo"] = "123",

    ["bar:gender"] = "Male",
    ["bar:age"] = "25",
    ["bar:contactInfo:emailAddress"] = "bar@outlook.com",
    ["bar:contactInfo:phoneNo"] = "456",

    ["baz:gender"] = "Female",
    ["baz:age"] = "36",
    ["baz:contactInfo:emailAddress"] = "baz@outlook.com",
    ["baz:contactInfo:phoneNo"] = "789"
};

var profiles = new ConfigurationBuilder()
    .AddInMemoryCollection(source)
    .Build()
    .Get<IDictionary<string,Profile>>();;

Debug.Assert(profiles["foo"].ContactInfo.EmailAddress == "foo@outlook.com");
Debug.Assert(profiles["bar"].ContactInfo.EmailAddress == "bar@outlook.com");
Debug.Assert(profiles["baz"].ContactInfo.EmailAddress == "baz@outlook.com");