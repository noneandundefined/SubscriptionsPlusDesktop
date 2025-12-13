using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionPlusDesktop.Utilities
{
    public class ParseShortDateUtilities
    {
        public static DateTime ParseShortDate(string input)
        {
            var parts = input.Split('.');

            if (parts.Length != 2) throw new FormatException("Неправильный формат. Используйте dd.MM");

            int day = int.Parse(parts[0]);
            int month = int.Parse(parts[1]);
            int year = DateTime.Now.Year;

            return new DateTime(year, month, day);
        }
    }
}
