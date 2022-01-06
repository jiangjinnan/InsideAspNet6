using App;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

var source = new Dictionary<string, string>
{
    ["0:gender"] = "Male",
    ["0:age"] = "18",
    ["0:contactInfo:emailAddress"] = "foo@outlook.com",
    ["0:contactInfo:phoneNo"] = "123",

    ["1:gender"] = "Male",
    ["1:age"] = "25",
    ["1:contactInfo:emailAddress"] = "bar@outlook.com",
    ["1:contactInfo:phoneNo"] = "456",

    ["2:gender"] = "Female",
    ["2:age"] = "36",
    ["2:contactInfo:emailAddress"] = "baz@outlook.com",
    ["2:contactInfo:phoneNo"] = "789"
};

var configuration = new ConfigurationBuilder()
    .AddInMemoryCollection(source)
    .Build();

var list = configuration.Get<IList<Profile>>();
Debug.Assert(list[0].ContactInfo.EmailAddress == "foo@outlook.com");
Debug.Assert(list[1].ContactInfo.EmailAddress == "bar@outlook.com");
Debug.Assert(list[2].ContactInfo.EmailAddress == "baz@outlook.com");

var array = configuration.Get<Profile[]>();
Debug.Assert(array[0].ContactInfo.EmailAddress == "foo@outlook.com");
Debug.Assert(array[1].ContactInfo.EmailAddress == "bar@outlook.com");
Debug.Assert(array[2].ContactInfo.EmailAddress == "baz@outlook.com");
