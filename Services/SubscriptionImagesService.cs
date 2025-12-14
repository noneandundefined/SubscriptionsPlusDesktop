using SubscriptionPlusDesktop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SubscriptionPlusDesktop.Services
{
    public class SubscriptionImagesService
    {
        private static readonly List<SubscriptionImageModel> SubscriptionImages = new List<SubscriptionImageModel>()
        {
            new SubscriptionImageModel
            {
                Keywords = new List<string>
                {
                    "yandex plus", "yandex+", "яндекс плюс", "яндекс+", "yandex music", "яндекс музыка",
                    "yandex подписка", "яндекс подписка", "yplus", "yandex", "yandex premium",
                    "яндекс премиум", "yandex pl", "яндекс pl", "яндексплюс", "yandexplus"
                },
                Image = "yplus.png"
            },
            new SubscriptionImageModel
            {
                Keywords = new List<string>
                {
                    "sber", "sber prime", "sberprime", "сбер прайм", "сберпрайм",
                    "sber подписка", "сбер подписка", "сбербанк подписка", "сбербанк"
                },
                Image = "sber.jpg"
            },
            new SubscriptionImageModel
            {
                Keywords = new List<string>
                {
                    "subscription plus", "sub plus", "subscription+", "subplus",
                    "подписка плюс", "subscriptionplus", "sub plus app"
                },
                Image = "subscriptionplus.png"
            },
            new SubscriptionImageModel
            {
                Keywords = new List<string>
                {
                    "tbank", "t-pro", "t pro", "t-bank", "tinkoff", "тинькоф", "тинкоф", "тинкофф", "тинькофф",
                    "tinkoff pro", "tinkoff black", "tbank подписка", "tinkoff подписка"
                },
                Image = "tbank.jpg"
            },

            // Telegram
            new SubscriptionImageModel
            {
                Keywords = new List<string>
                {
                    "telegram", "telegram premium", "tg premium", "tg+", "tg",
                    "телеграм премиум", "тг премиум", "tg подписка", "telegram подписка"
                },
                Image = "tg.png"
            },

            // Spotify
            new SubscriptionImageModel
            {
                Keywords = new List<string>
                {
                    "spotify", "spoti", "spoti fy", "спотифай", "споттифай",
                    "spotify premium", "spotify подписка", "споти премиум"
                },
                Image = "spotify.jpg"
            },

            // Netflix
            new SubscriptionImageModel
            {
                Keywords = new List<string>
                {
                    "netflix", "нетфликс", "нетфликс подписка", "netflix premium", "netflix ultra",
                    "net flix", "net flix подписка"
                },
                Image = "netflix.jpg"
            },

            // YouTube Premium
            new SubscriptionImageModel
            {
                Keywords = new List<string>
                {
                    "youtube", "youtube premium", "ютуб премиум", "ютуб+", "yt premium",
                    "youtube+", "ютуб подписка", "youtube music"
                },
                Image = "youtube.png"
            },

            // Apple One / Music / TV+
            new SubscriptionImageModel
            {
                Keywords = new List<string>
                {
                    "apple", "apple one", "apple music", "apple tv+", "apple tv plus",
                    "apple подписка", "эпл музыка", "эпл подписка"
                },
                Image = "apple.png"
            },

            // Google One
            new SubscriptionImageModel
            {
                Keywords = new List<string>
                {
                    "google one", "гугл one", "гугл подписка", "google диск", "google storage", "google облако", "google"
                },
                Image = "googleone.jpg"
            },

            // Microsoft 365 / Office 365
            new SubscriptionImageModel
            {
                Keywords = new List<string>
                {
                    "microsoft 365", "office 365", "microsoft подписка", "офис 365", "microsoft one drive"
                },
                Image = "office365.jpg"
            },

            // Adobe
            new SubscriptionImageModel
            {
                Keywords = new List<string>
                {
                    "adobe", "adobe cc", "creative cloud", "фотошоп подписка", "adobe подписка"
                },
                Image = "adobe.jpg"
            },

            // Discord Nitro
            new SubscriptionImageModel
            {
                Keywords = new List<string>
                {
                    "discord", "discord nitro", "nitro", "дискорд нитро", "дискорд подписка"
                },
                Image = "discord.png"
            },

            // Steam
            new SubscriptionImageModel
            {
                Keywords = new List<string> { "steam", "steam subscription", "steam подписка", "steam premium" },
                Image = "steam.png"
            },

            // Xbox
            new SubscriptionImageModel
            {
                Keywords = new List<string> { "xbox", "xbox game pass", "gamepass", "геймпас", "xbox подписка" },
                Image = "xbox.jpg"
            },

            // PlayStation / PS Plus
            new SubscriptionImageModel
            {
                Keywords = new List<string> { "playstation", "ps plus", "пс плюс", "плейстейшн плюс", "ps подписка" },
                Image = "psplus.jpg"
            },

            // IVI / Okko / Kinopoisk
            new SubscriptionImageModel
            {
                Keywords = new List<string> { "ivi", "иви", "ivi подписка" },
                Image = "ivi.jpg"
            },
            new SubscriptionImageModel
            {
                Keywords = new List<string> { "okko", "окко", "okko подписка", "окко подписка" },
                Image = "okko.png"
            },
            new SubscriptionImageModel
            {
                Keywords = new List<string> { "kinopoisk", "кино поиск", "кинопоиск", "кинопоиск подписка" },
                Image = "kinopoisk.jpg"
            },

            // VPN
            new SubscriptionImageModel
            {
                Keywords = new List<string>
                {
                    "vpn", "v p n", "впн", "прокси", "proxy", "vpn service", "vpn подписка",
                    "vpn premium", "windscribe", "nordvpn", "surfshark", "expressvpn", "vpn plus"
                },
                Image = "vpn.jpg"
            },

            // Cloud / VDS / VPS
            new SubscriptionImageModel
            {
                Keywords = new List<string>
                {
                    "vds", "виртуальный сервер", "дедик", "dedicated", "dedicated server",
                    "vdsina", "timeweb", "reg.ru vps", "reg vps"
                },
                Image = "vds.jpg"
            },
            new SubscriptionImageModel
            {
                Keywords = new List<string> { "vps", "сервер", "reg.ru vps", "reg vps" },
                Image = "vps.jpg"
            },

            // AI / ChatGPT / Claude / Midjourney
            new SubscriptionImageModel
            {
                Keywords = new List<string> { "chatgpt", "gpt plus", "chat gpt+", "openai plus", "чатгпт подписка", "openai" },
                Image = "chatgpt.png"
            },
            new SubscriptionImageModel
            {
                Keywords = new List<string> { "midjourney", "mid journey", "midjourney подписка" },
                Image = "midjourney.jpg"
            },
            new SubscriptionImageModel
            {
                Keywords = new List<string> { "claude", "anthropic", "claude ai", "claude подписка" },
                Image = "claude.jpg"
            },

            // MTS
            new SubscriptionImageModel
            {
                Keywords = new List<string> { "mts", "mts подписка", "мтс", "мтс подписка", "mts premium" },
                Image = "mts.jpg"
            },

            // Beeline
            new SubscriptionImageModel
            {
                Keywords = new List<string> { "beeline", "beeline подписка", "билайн", "билайн подписка", "beeline premium" },
                Image = "beeline.png"
            },

            // T2
            new SubscriptionImageModel
            {
                Keywords = new List<string> { "t2", "t2 подписка", "t2 premium", "т2", "т2 подписка", "tele2", "tele 2" },
                Image = "t2.png"
            },

            // MegaFon
            new SubscriptionImageModel
            {
                Keywords = new List<string> { "megafon", "mega fon", "мегафон", "мегафон подписка", "megafon подписка" },
                Image = "megafon.png"
            },

            // Dom.ru
            new SubscriptionImageModel
            {
                Keywords = new List<string> { "dom.ru", "дом ру", "дом.ру", "dom ru", "интернет dom.ru", "тв dom.ru" },
                Image = "domru.png"
            },

            // Ozon Premium
            new SubscriptionImageModel
            {
                Keywords = new List<string> { "ozon", "ozon premium", "ozon подписка", "озон", "озон премиум" },
                Image = "ozon.png"
            },

            // Wildberries Plus
            new SubscriptionImageModel
            {
                Keywords = new List<string> { "wb", "wildberries", "wildberries plus", "wb подписка", "вб", "вб премиум" },
                Image = "wb.png"
            },

            // Транспортная карта
            new SubscriptionImageModel
            {
                Keywords = new List<string> { "transport card", "транспортная карта" },
                Image = "buscard.png"
            },

            // GeekBrains
            new SubscriptionImageModel
            {
                Keywords = new List<string> { "geekbrains", "geekbrains курс", "geekbrains обучение", "гикбрейнс" },
                Image = "geekbrains.png"
            },

            // Stepik
            new SubscriptionImageModel
            {
                Keywords = new List<string> { "stepik", "stepik курс", "stepik обучение", "степик" },
                Image = "stepik.jpg"
            },

            // Samokat
            new SubscriptionImageModel
            {
                Keywords = new List<string> { "samokat", "самокат", "samokat доставка", "самокат доставка" },
                Image = "samokat.png"
            },

            // VK / VK Video
            new SubscriptionImageModel
            {
                Keywords = new List<string> { "vk", "вк", "vk video", "вк видео", "vk подписка", "вк премиум" },
                Image = "vk.png"
            },

            // Деньги / финансы
            new SubscriptionImageModel
            {
                Keywords = new List<string> { "перевод", "переводы", "должен", "займ", "кредит", "долг" },
                Image = "money.png"
            },
        };

        public string GetSubscriptionImage(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return string.Empty;

            var processed = input.ToLower();

            foreach (var sub in SubscriptionImages)
            {
                foreach (var kw in sub.Keywords)
                {
                    if (processed.Contains(kw.ToLower())) return sub.Image;
                }
            }

            return string.Empty;
        }
    }
}
