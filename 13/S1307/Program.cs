using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

var password = "password";
var salt = new byte[16];
var iteration = 1000;

using (var generator = RandomNumberGenerator.Create())
{
    generator.GetBytes(salt);
}

Console.WriteLine(Hash(KeyDerivationPrf.HMACSHA1));
Console.WriteLine(Hash(KeyDerivationPrf.HMACSHA256));
Console.WriteLine(Hash(KeyDerivationPrf.HMACSHA512));

string Hash(KeyDerivationPrf prf)
{
    var hashed = KeyDerivation.Pbkdf2(
        password: password,
        salt: salt,
        prf: prf,
        iterationCount: iteration,
        numBytesRequested: 32);
    return Convert.ToBase64String(hashed);
}
