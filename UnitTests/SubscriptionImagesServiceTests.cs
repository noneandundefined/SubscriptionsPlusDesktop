using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubscriptionPlusDesktop.Services;
using System;

namespace UnitTests
{
    [TestClass]
    public class SubscriptionImagesServiceTests
    {
        private SubscriptionImagesService _service;

        [TestInitialize]
        public void Init()
        {
            this._service = new SubscriptionImagesService();
        }

        [DataTestMethod]
        [DataRow("Yandex Plus", "yplus.png")]
        [DataRow("яндекс музыка", "yplus.png")]
        [DataRow("Spotify Premium", "spotify.jpg")]
        [DataRow("Netflix Ultra", "netflix.jpg")]
        [DataRow("Telegram Premium", "tg.png")]
        [DataRow("Вернуть долг", "money.png")]
        [DataRow("Тинкоф Про", "tbank.jpg")]
        [DataRow("T-Pro", "tbank.jpg")]
        [DataRow("гугл подписка", "googleone.jpg")]
        [DataRow("adobe ПОДписка", "adobe.jpg")]
        [DataRow("adobe ПРО", "adobe.jpg")]
        [DataRow("steam подписка", "steam.png")]
        public void GetSubscriptionImage_KnownSubscription_ReturnsImage(string input, string expected)
        {
            var result = this._service.GetSubscriptionImage(input);

            Assert.AreEqual(expected, result);
        }

        [DataTestMethod]
        [DataRow("Unknown Service")]
        [DataRow("Random Text")]
        public void GetSubscriptionImage_Unknown_ReturnsEmpty(string input)
        {
            var result = this._service.GetSubscriptionImage(input);

            Assert.AreEqual(string.Empty, result);
        }

        [DataTestMethod]
        [DataRow("")]
        [DataRow("   ")]
        [DataRow(null)]
        public void GetSubscriptionImage_EmptyOrNull_ReturnsEmpty(string input)
        {
            var result = this._service.GetSubscriptionImage(input);

            Assert.AreEqual(string.Empty, result);
        }
    }
}
