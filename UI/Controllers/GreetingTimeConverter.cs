using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SubscriptionPlusDesktop.UI.Controllers
{
    public class GreetingTimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var hour = DateTime.Now.Hour;

            if (hour >= 5 && hour < 12) return "Доброе утро!";

            if (hour >= 12 && hour < 18) return "Добрый день!";

            if (hour >= 18 && hour < 23) return "Добрый вечер!";

            return "Доброй ночи!";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
