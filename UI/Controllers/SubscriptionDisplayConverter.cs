using SubscriptionPlusDesktop.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace SubscriptionPlusDesktop.UI.Controllers
{
    public class SubscriptionDisplayConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is SubscriptionModel sub)
            {
                string date = sub.DatePay.ToString("dd MMMM", new CultureInfo("ru-RU"));
                string price = $"{sub.Price} руб.";
                return $"{date} / {price}";
            }

            return "";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
