using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandomUtils
{
    public class DateUtils
    {
        private static string dateFormat = "yyyy-MM-dd-hh-mm-ss";

        public static string GetDate()
        {
            return DateTime.Now.ToString(dateFormat);
        }

        public static DateTime ParseToDateTime(string date)
        {
            return DateTime.ParseExact(date, dateFormat, null);
        }
    }
}
