using SubscriptionPlusDesktop.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace SubscriptionPlusDesktop.UI.Controllers
{
    public class SubscriptionImageConverter : IValueConverter
    {
        private readonly SubscriptionImagesService _subImageService = new SubscriptionImagesService();

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string subscriptionName)
            {
                var imageFile = _subImageService.GetSubscriptionImage(subscriptionName);

                if (!string.IsNullOrEmpty(imageFile))
                {
                    return new ImageBrush
                    {
                        ImageSource = new BitmapImage(new Uri($"pack://application:,,,/Public/media/{imageFile}"))
                    };
                }
            }

            return new SolidColorBrush(System.Windows.Media.Colors.Transparent);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
