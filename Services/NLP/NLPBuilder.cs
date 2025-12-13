using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SubscriptionPlusDesktop.Services.NLP
{
    public class NLPBuilder : NLPData
    {
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
                    if (part.Length <= 2 || this._stopWords.Contains(part) || this.HasDigit(part)) continue;

                    var stemmed = this.Stemming(part);
                    var lemmatized = this.Lemmatize(stemmed);

                    if (lemmatized.Length > 2) output.Add(lemmatized);
                }
            }

            return output;
        }

        private string Lemmatize(string word)
        {
            if (this._lemmasDictionary.TryGetValue(word, out string lemma))
            {
                return lemma;
            }

            return word;
        }

        private string Stemming(string word)
        {
            // Engl.
            if (word.EndsWith("ational")) return word.Substring(0, word.Length - 7) + "ate";
            if (word.EndsWith("tional")) return word.Substring(0, word.Length - 6) + "tion";
            if (word.EndsWith("ization")) return word.Substring(0, word.Length - 7) + "ize";
            if (word.EndsWith("fulness")) return word.Substring(0, word.Length - 7);
            if (word.EndsWith("ousness")) return word.Substring(0, word.Length - 7);
            if (word.EndsWith("iveness")) return word.Substring(0, word.Length - 7);
            if (word.EndsWith("alism")) return word.Substring(0, word.Length - 5);
            if (word.EndsWith("alize")) return word.Substring(0, word.Length - 5) + "al";
            if (word.EndsWith("icate")) return word.Substring(0, word.Length - 5) + "ic";
            if (word.EndsWith("ative")) return word.Substring(0, word.Length - 5);
            if (word.EndsWith("ement")) return word.Substring(0, word.Length - 5);
            if (word.EndsWith("ingly")) return word.Substring(0, word.Length - 5);
            if (word.EndsWith("fully")) return word.Substring(0, word.Length - 5);
            if (word.EndsWith("eable")) return word.Substring(0, word.Length - 5);
            if (word.EndsWith("iable")) return word.Substring(0, word.Length - 5);
            if (word.EndsWith("ition")) return word.Substring(0, word.Length - 5) + "it";
            if (word.EndsWith("ation")) return word.Substring(0, word.Length - 5) + "ate";
            if (word.EndsWith("ator")) return word.Substring(0, word.Length - 4) + "ate";
            if (word.EndsWith("ator")) return word.Substring(0, word.Length - 4) + "ate";
            if (word.EndsWith("ment")) return word.Substring(0, word.Length - 4);
            if (word.EndsWith("ence")) return word.Substring(0, word.Length - 4);
            if (word.EndsWith("ance")) return word.Substring(0, word.Length - 4);
            if (word.EndsWith("ably")) return word.Substring(0, word.Length - 4);
            if (word.EndsWith("ibly")) return word.Substring(0, word.Length - 4);
            if (word.EndsWith("izer")) return word.Substring(0, word.Length - 4);

            // Rus.
            if (word.EndsWith("ованный")) return word.Substring(0, word.Length - 7);
            if (word.EndsWith("енный")) return word.Substring(0, word.Length - 5);
            if (word.EndsWith("ющим")) return word.Substring(0, word.Length - 4);
            if (word.EndsWith("аешь")) return word.Substring(0, word.Length - 4);
            if (word.EndsWith("ишься")) return word.Substring(0, word.Length - 5);
            if (word.EndsWith("ются")) return word.Substring(0, word.Length - 4);
            if (word.EndsWith("ываться")) return word.Substring(0, word.Length - 7);
            if (word.EndsWith("иваться")) return word.Substring(0, word.Length - 7);
            if (word.EndsWith("ся")) return word.Substring(0, word.Length - 2);

            if (word.EndsWith("ющего")) return word.Substring(0, word.Length - 5);
            if (word.EndsWith("аются")) return word.Substring(0, word.Length - 5);
            if (word.EndsWith("еются")) return word.Substring(0, word.Length - 5);

            if (word.EndsWith("ать")) return word.Substring(0, word.Length - 3);
            if (word.EndsWith("ять")) return word.Substring(0, word.Length - 3);
            if (word.EndsWith("ить")) return word.Substring(0, word.Length - 3);
            if (word.EndsWith("ыть")) return word.Substring(0, word.Length - 3);
            if (word.EndsWith("еть")) return word.Substring(0, word.Length - 3);
            if (word.EndsWith("ость")) return word.Substring(0, word.Length - 4);
            if (word.EndsWith("ование")) return word.Substring(0, word.Length - 6);
            if (word.EndsWith("ствие")) return word.Substring(0, word.Length - 5);

            if (word.EndsWith("ого")) return word.Substring(0, word.Length - 3);
            if (word.EndsWith("его")) return word.Substring(0, word.Length - 3);
            if (word.EndsWith("ему")) return word.Substring(0, word.Length - 3);
            if (word.EndsWith("ому")) return word.Substring(0, word.Length - 3);
            if (word.EndsWith("ими")) return word.Substring(0, word.Length - 3);
            if (word.EndsWith("ыми")) return word.Substring(0, word.Length - 3);
            if (word.EndsWith("ешь")) return word.Substring(0, word.Length - 4);
            if (word.EndsWith("ое")) return word.Substring(0, word.Length - 2);
            if (word.EndsWith("ие")) return word.Substring(0, word.Length - 2);
            if (word.EndsWith("ые")) return word.Substring(0, word.Length - 2);

            if (word.EndsWith("ий")) return word.Substring(0, word.Length - 2);
            if (word.EndsWith("ый")) return word.Substring(0, word.Length - 2);
            if (word.EndsWith("ая")) return word.Substring(0, word.Length - 2);
            if (word.EndsWith("яя")) return word.Substring(0, word.Length - 2);
            if (word.EndsWith("ое")) return word.Substring(0, word.Length - 2);
            if (word.EndsWith("ее")) return word.Substring(0, word.Length - 2);

            if (word.EndsWith("ем")) return word.Substring(0, word.Length - 2);
            if (word.EndsWith("ом")) return word.Substring(0, word.Length - 2);
            if (word.EndsWith("ам")) return word.Substring(0, word.Length - 2);
            if (word.EndsWith("ям")) return word.Substring(0, word.Length - 2);
            if (word.EndsWith("ах")) return word.Substring(0, word.Length - 2);
            if (word.EndsWith("ях")) return word.Substring(0, word.Length - 2);
            if (word.EndsWith("ой")) return word.Substring(0, word.Length - 2);
            if (word.EndsWith("ей")) return word.Substring(0, word.Length - 2);

            if (word.EndsWith("ет")) return word.Substring(0, word.Length - 2);
            if (word.EndsWith("ит")) return word.Substring(0, word.Length - 2);
            if (word.EndsWith("ут")) return word.Substring(0, word.Length - 2);
            if (word.EndsWith("ют")) return word.Substring(0, word.Length - 2);
            if (word.EndsWith("ат")) return word.Substring(0, word.Length - 2);
            if (word.EndsWith("ят")) return word.Substring(0, word.Length - 2);

            if (word.EndsWith("ing"))
            {
                string stem = word.Substring(0, word.Length - 3);
                if (!string.IsNullOrEmpty(stem) && !this.IsVowel(stem[stem.Length - 1])) return stem + "e";
                return stem;
            }
            if (word.EndsWith("ies")) return word.Substring(0, word.Length - 3) + "y";
            if (word.EndsWith("ive")) return word.Substring(0, word.Length - 3);
            if (word.EndsWith("ed"))
            {
                string stem = word.Substring(0, word.Length - 2);
                if (!string.IsNullOrEmpty(stem) && !this.IsVowel(stem[stem.Length - 1])) return stem + "e";
                return stem;
            }
            if (word.EndsWith("es")) return word.Substring(0, word.Length - 2);
            if (word.EndsWith("ly")) return word.Substring(0, word.Length - 2);
            if (word.EndsWith("s"))
            {
                if (word.Length > 2 && !word.EndsWith("ss")) return word.Substring(0, word.Length - 1);
            }
            if (word.EndsWith("а")) return word.Substring(0, word.Length - 1);
            if (word.EndsWith("я")) return word.Substring(0, word.Length - 1);
            if (word.EndsWith("и")) return word.Substring(0, word.Length - 1);
            if (word.EndsWith("ы")) return word.Substring(0, word.Length - 1);
            if (word.EndsWith("о")) return word.Substring(0, word.Length - 1);
            if (word.EndsWith("е")) return word.Substring(0, word.Length - 1);
            if (word.EndsWith("у")) return word.Substring(0, word.Length - 1);
            if (word.EndsWith("ю")) return word.Substring(0, word.Length - 1);


            return word;
        }

        private bool IsVowel(char c) => "aeiou".Contains(c);

        private bool HasDigit(string s) => s.Any(char.IsDigit);

        private int CalculateLevenshteinDistance(string s, string t)
        {
            if (string.IsNullOrEmpty(s))
            {
                if (string.IsNullOrEmpty(t)) return 0;
                return t.Length;
            }

            if (string.IsNullOrEmpty(t)) return s.Length;

            int n = s.Length;
            int m = t.Length;
            int[,] d = new int[n + 1, m + 1];

            for (int i = 0; i <= n; d[i, 0] = i++) ;
            for (int j = 0; j <= m; d[0, j] = j++) ;

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    int cost = (t[j - 1] == s[i - 1]) ? 0 : 1;
                    d[i, j] = Math.Min(Math.Min(d[i - 1, j] + 1, d[i, j - 1] + 1), d[i - 1, j - 1] + cost);
                }
            }

            return d[n, m];
        }

        public bool IsFuzzyMatch(string source, string target, int threshold)
        {
            if (source.Length <= 2 || target.Length <= 2)
            {
                return source.Equals(target, StringComparison.OrdinalIgnoreCase);
            }

            int distance = this.CalculateLevenshteinDistance(source, target);
            return distance <= threshold;
        }
    }
}
