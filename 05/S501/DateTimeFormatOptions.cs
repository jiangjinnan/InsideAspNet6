using Microsoft.Extensions.Configuration;

namespace App
{
    public class DateTimeFormatOptions
    {
        public string LongDatePattern { get; set; }
        public string LongTimePattern { get; set; }
        public string ShortDatePattern { get; set; }
        public string ShortTimePattern { get; set; }

        public DateTimeFormatOptions(IConfiguration config)
        {
            LongDatePattern = config["LongDatePattern"];
            LongTimePattern = config["LongTimePattern"];
            ShortDatePattern = config["ShortDatePattern"];
            ShortTimePattern = config["ShortTimePattern"];
        }
    }
}