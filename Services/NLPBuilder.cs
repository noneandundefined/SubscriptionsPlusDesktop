using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SubscriptionPlusDesktop.Services
{
    public class NLPBuilder
    {
        private readonly HashSet<string> _stopWords;

        public NLPBuilder()
        {
            var words = new[]
            {
                "i","me","my","myself","we","our","ours","us","this","these","those",
                "he","she","it","its","itself","them","their","by","from","into","during",
                "after","before","above","below","but","or","as","if","then","else","so",
                "will","would","shall","should","can","could","may","might","must",
                "here","there","now","just","very","too","about","again","once","been",
                "being","both","each","few","more","most","other","some","such","what",
                "which","why","how","the"
            };

            this._stopWords = new HashSet<string>(words);
        }

        public List<string> Preprocess(string input)
        {
            if (string.IsNullOrWhiteSpace(input)) return new List<string>();

            input = Regex.Replace(input, @"[()<>""{}\[\]]", " ");

            var words = Regex.Split(input.ToLower(), @"[\s,\.!?;:\t\n\r]+");

            var output = new List<string>();

            foreach (var word in words)
            {
                foreach (var part in word.Split('-'))
                {
                    if (part.Length <= 2 || _stopWords.Contains(part) || this.HasDigit(part)) continue;

                    var stemmed = this.Stemming(part);
                    var lemmatized = this.Lemmatize(stemmed);

                    if (lemmatized.Length > 2) output.Add(lemmatized);
                }
            }

            return output;
        }

        private string Lemmatize(string word)
        {
            switch (word)
            {
                case "am":
                case "is":
                case "are":
                case "was":
                case "were":
                    return "be";
                case "has":
                case "have":
                case "had":
                    return "have";
                case "does":
                case "did":
                    return "do";
                case "going":
                case "goes":
                case "went":
                    return "go";
                case "made":
                case "making":
                    return "make";
                case "saw":
                case "seen":
                case "seeing":
                    return "see";
                case "came":
                case "coming":
                    return "come";
                case "took":
                case "takes":
                case "taken":
                case "taking":
                    return "take";
                case "better":
                case "best":
                    return "good";
                case "worse":
                case "worst":
                    return "bad";
                case "bigger":
                case "biggest":
                    return "big";
                case "smaller":
                case "smallest":
                    return "small";
                case "larger":
                case "largest":
                    return "large";
                case "more":
                case "most":
                    return "many";
                case "less":
                case "least":
                    return "little";
                case "further":
                case "furthest":
                    return "far";
                case "children":
                    return "child";
                case "people":
                    return "person";
                case "lives":
                    return "life";
                case "wives":
                    return "wife";
                case "fewer":
                case "fewest":
                    return "few";
                default:
                    return word;
            }
        }

        private string Stemming(string word)
        {
            if (word.EndsWith("fulness"))
                return word.Substring(0, word.Length - 7);
            if (word.EndsWith("ousness"))
                return word.Substring(0, word.Length - 7);
            if (word.EndsWith("ization"))
                return word.Substring(0, word.Length - 7) + "ize";
            if (word.EndsWith("ational"))
                return word.Substring(0, word.Length - 7) + "ate";
            if (word.EndsWith("tional"))
                return word.Substring(0, word.Length - 6) + "tion";
            if (word.EndsWith("alize"))
                return word.Substring(0, word.Length - 5) + "al";
            if (word.EndsWith("icate"))
                return word.Substring(0, word.Length - 5) + "ic";
            if (word.EndsWith("ative"))
                return word.Substring(0, word.Length - 5);
            if (word.EndsWith("ement"))
                return word.Substring(0, word.Length - 5);
            if (word.EndsWith("ingly"))
                return word.Substring(0, word.Length - 5);
            if (word.EndsWith("fully"))
                return word.Substring(0, word.Length - 5);
            if (word.EndsWith("ably"))
                return word.Substring(0, word.Length - 4);
            if (word.EndsWith("ibly"))
                return word.Substring(0, word.Length - 4);
            if (word.EndsWith("ing"))
            {
                string stem = word.Substring(0, word.Length - 3);
                if (!string.IsNullOrEmpty(stem) && !this.IsVowel(stem[stem.Length - 1])) return stem + "e";
                return stem;
            }
            if (word.EndsWith("ies"))
                return word.Substring(0, word.Length - 3) + "y";
            if (word.EndsWith("ive"))
                return word.Substring(0, word.Length - 3);
            if (word.EndsWith("es"))
                return word.Substring(0, word.Length - 2);
            if (word.EndsWith("ly"))
                return word.Substring(0, word.Length - 2);
            if (word.EndsWith("ed"))
            {
                string stem = word.Substring(0, word.Length - 2);
                if (!string.IsNullOrEmpty(stem) && !this.IsVowel(stem[stem.Length - 1])) return stem + "e";
                return stem;
            }

            return word;
        }

        private bool IsVowel(char c) => "aeiou".Contains(c);

        private bool HasDigit(string s) => s.Any(char.IsDigit);
    }
}
