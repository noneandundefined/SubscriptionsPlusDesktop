using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionPlusDesktop.Models
{
    public static class SubscriptionCategories
    {
        public static List<string> Categories { get; } = new List<string>
        {
            "Музыка",
            "Видео",
            "Образование",
            "Игры",
            "Другое"
        };
    }
}
