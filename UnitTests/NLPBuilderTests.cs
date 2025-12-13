using Microsoft.VisualStudio.TestTools.UnitTesting;
using SubscriptionPlusDesktop.Services.NLP;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class NLPBuilderTests
    {
        private NLPBuilder _nlp;

        [TestInitialize]
        public void Init()
        {
            this._nlp = new NLPBuilder();
        }

        [TestMethod]
        public void Preprocess_EmptyInput_ReturnsEmptyList()
        {
            var result = this._nlp.Preprocess("");

            Assert.AreEqual(0, result.Count);
        }

        [TestMethod]
        public void Preprocess_RemovesStopWords()
        {
            var result = this._nlp.Preprocess("this is a test of the system");

            CollectionAssert.DoesNotContain(result, "this");
            CollectionAssert.DoesNotContain(result, "is");
            CollectionAssert.Contains(result, "test");
            CollectionAssert.Contains(result, "system");
        }

        [TestMethod]
        public void Preprocess_RemovesDigits()
        {
            var result = this._nlp.Preprocess("vpn123 subscription 2024");

            Assert.IsFalse(result.Any(w => w.Any(char.IsDigit)));
        }

        [TestMethod]
        public void Preprocess_StemmingWorks()
        {
            var result = this._nlp.Preprocess("running");

            Assert.IsTrue(result.Contains("run") || result.Contains("runne"));
        }
    }
}
