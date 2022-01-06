namespace App
{
public readonly record struct Profile(Gender Gender, int Age, ContactInfo ContactInfo);
public readonly record struct ContactInfo(string EmailAddress, string PhoneNo);
public enum Gender
{
    Male,
    Female
}
}