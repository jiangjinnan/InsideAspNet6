namespace App
{
    public class Profile
    {
        public Gender Gender { get; set; }
        public int Age { get; set; }
        public ContactInfo? ContactInfo { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }

    public class ContactInfo
    {
        public string? EmailAddress { get; set; }
        public string? PhoneNo { get; set; }
    }

}