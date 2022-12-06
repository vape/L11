using System;
using System.Collections.Generic;

namespace L11
{
    // https://www.gnu.org/software/gettext/manual/gettext.html#Plural-forms
    public static class Plural
    {
        public abstract class PluralRule
        {
            public abstract int FormsCount
            { get; }

            public abstract int GetForm(long n);
        }

        public class CustomRule : PluralRule
        {
            public override int FormsCount => count;

            private int count;
            private Func<long, int> equation;

            public CustomRule(int count, Func<long, int> equation)
            {
                this.count = count;
                this.equation = equation;
            }

            public override int GetForm(long value)
            {
                return equation(value);
            }
        }

        private class OneFormRule : PluralRule
        {
            public override int FormsCount => 1;

            public override int GetForm(long n)
            {
                return 0;
            }
        }

        private class TwoForms1Rule : PluralRule
        {
            public override int FormsCount => 2;

            public override int GetForm(long n)
            {
                return n == 1 ? 1 : 0;
            }
        }

        private class TwoForms2Rule : PluralRule
        {
            public override int FormsCount => 2;

            public override int GetForm(long n)
            {
                return n == 0 || n == 1 ? 0 : 1;
            }
        }

        private class ThreeForms1Rule : PluralRule
        {
            public override int FormsCount => 3;

            public override int GetForm(long n)
            {
                return
                    n % 10 == 1 && n % 100 != 11 ? 0 :
                    n % 10 >= 2 && n % 10 <= 4 && (n % 100 < 10 || n % 100 >= 20) ? 1 : 2;
            }
        }

        private class ThreeForms2Rule : PluralRule
        {
            public override int FormsCount => 3;

            public override int GetForm(long n)
            {
                return n == 1 ? 0 : n >= 2 && n <= 4 ? 1 : 2;
            }
        }

        private static string[] oneForm = new string[]
        {
            // Aymará
            "ay",
            // Tibetan
            "bo",
            // Chiga
            "cgg",
            // Dzongkha
            "dz",
            // Persian
            "fa",
            // Indonesian
            "id",
            //Japanese
            "ja",
            // Lojban
            "jbo",
            // Georgian
            "ka",
            // Kazakh
            "kk",
            // Khmer
            "km",
            // Korean
            "ko",
            // Kyrgyz
            "ky",
            // Lao
            "lo",
            // Malay
            "ms",
            // Burmese
            "my",
            // Yakut
            "sah",
            // Sundanese
            "su",
            // Thai
            "th",
            // Tatar
            "tt",
            // Uyghur
            "ug",
            // Vietnamese
            "vi",
            // Wolof
            "wo",
            // Chinese [2]
            "zh"
        };

        private static string[] twoForms1 = new string[]
        {
            // Afrikaans
            "af",
            // Aragonese
            "an",
            // Angika
            "anp",
            // Assamese
            "as",
            // Asturian
            "ast",
            // Azerbaijani
            "az",
            // Bulgarian
            "bg",
            // Bengali
            "bn",
            // Bodo
            "brx",
            // Catalan
            "ca",
            // Danish
            "da",
            // German
            "de",
            // Dogri
            "doi",
            // Greek
            "el",
            // English
            "en",
            // Esperanto
            "eo",
            // Spanish
            "es",
            // Estonian
            "et",
            // Basque
            "eu",
            // Fulah
            "ff",
            // Finnish
            "fi",
            // Faroese
            "fo",
            // Friulian
            "fur",
            // Frisian
            "fy",
            // Galician
            "gl",
            // Gujarati
            "gu",
            // Hausa
            "ha",
            // Hebrew
            "he",
            // Hindi
            "hi",
            // Chhattisgarhi
            "hne",
            // Armenian
            "hy",
            // Hungarian
            "hu",
            // Interlingua
            "ia",
            // Italian
            "it",
            // Greenlandic
            "kl",
            // Kannada
            "kn",
            // Kurdish
            "ku",
            // Letzeburgesch
            "lb",
            // Maithili
            "mai",
            // Malayalam
            "ml",
            // Mongolian
            "mn",
            // Manipuri
            "mni",
            // Marathi
            "mr",
            // Nahuatl
            "nah",
            // Neapolitan
            "nap",
            // Norwegian Bokmal
            "nb",
            // Nepali
            "ne",
            // Dutch
            "nl",
            // Northern Sami
            "se",
            // Norwegian Nynorsk
            "nn",
            // Norwegian (old code)
            "no",
            // Northern Sotho
            "nso",
            // Oriya
            "or",
            // Pashto
            "ps",
            // Punjabi
            "pa",
            // Papiamento
            "pap",
            // Piemontese
            "pms",
            // Portuguese
            "pt",
            // Romansh
            "rm",
            // Kinyarwanda
            "rw",
            // Santali
            "sat",
            // Scots
            "sco",
            // Sindhi
            "sd",
            // Sinhala
            "si",
            // Somali
            "so",
            // Songhay
            "son",
            // Albanian
            "sq",
            // Swahili
            "sw",
            // Swedish
            "sv",
            // Tamil
            "ta",
            // Telugu
            "te",
            // Turkmen
            "tk",
            // Urdu
            "ur",
            // Yoruba
            "yo"
        };

        private static string[] twoForms2 = new string[]
        {
            // Acholi
            "ach",
            // Akan
            "ak",
            // Amharic
            "am",
            // Mapudungun
            "arn",
            // Breton
            "br",
            // Filipino
            "fil",
            // French
            "fr",
            // Gun
            "gun",
            // Lingala
            "ln",
            // Mauritian Creole
            "mfe",
            // Malagasy
            "mg",
            // Maori
            "mi",
            // Occitan
            "oc",
            // Tajik
            "tg",
            // Tigrinya
            "ti",
            // Tagalog
            "tl",
            // Turkish
            "tr",
            // Uzbek
            "uz",
            // Walloon
            "wa"
        };

        private static string[] threeForms1 = new string[]
        {
            // Belarusian
            "be",
            // Bosnian
            "bs",
            // Croatian
            "hr",
            // Serbian
            "sr",
            // Russian
            "ru",
            // Ukrainian
            "uk"
        };

        private static string[] threeForms2 = new string[]
        {
            // Czech
            "cs",
            // Slovak
            "sk"
        };

        private static Dictionary<string, PluralRule> BuildRules()
        {
            var rules = new Dictionary<string, PluralRule>(capacity:
                oneForm.Length +
                twoForms1.Length +
                twoForms2.Length +
                threeForms1.Length +
                threeForms2.Length +
                16);

            var oneFormRule = new OneFormRule();
            var twoForms1Rule = new TwoForms1Rule();
            var twoForms2Rule = new TwoForms2Rule();
            var threeFormsSlavicRule = new ThreeForms1Rule();
            var threeFormsSlavicAltRule = new ThreeForms2Rule();

            foreach (var l in oneForm)
            {
                rules.Add(l, oneFormRule);
            }

            foreach (var l in twoForms1)
            {
                rules.Add(l, twoForms1Rule);
            }

            foreach (var l in twoForms2)
            {
                rules.Add(l, twoForms2Rule);
            }

            foreach (var l in threeForms1)
            {
                rules.Add(l, threeFormsSlavicRule);
            }

            foreach (var l in threeForms2)
            {
                rules.Add(l, threeFormsSlavicAltRule);
            }

            // Arabic
            rules.Add("ar", new CustomRule(6, (n) =>  n == 0 ? 0 : n == 1 ? 1 : n == 2 ? 2 : n % 100 >= 3 && n % 100 <= 10 ? 3 : n % 100 >= 11 ? 4 : 5));
            // Kashubian
            rules.Add("csb", new CustomRule(3, (n) => (n == 1) ? 0 : n % 10 >= 2 && n % 10 <= 4 && (n % 100 < 10 || n % 100 >= 20) ? 1 : 2));
            // Welsh
            rules.Add("cy", new CustomRule(4, (n) => (n == 1) ? 0 : (n == 2) ? 1 : (n != 8 && n != 11) ? 2 : 3));
            // Irish
            rules.Add("ga", new CustomRule(5, (n) => n == 1 ? 0 : n == 2 ? 1 : (n > 2 && n < 7) ? 2 : (n > 6 && n < 11) ? 3 : 4));
            // Scottish Gaelic
            rules.Add("gd", new CustomRule(4, (n) => (n == 1 || n == 11) ? 0 : (n == 2 || n == 12) ? 1 : (n > 2 && n < 20) ? 2 : 3));
            // Icelandic
            rules.Add("is", new CustomRule(2, (n) => n % 10 != 1 || n % 100 == 11 ? 1 : 0));
            // Javanese
            rules.Add("jv", new CustomRule(2, (n) => n == 0 ? 0 : 1));
            // Cornish
            rules.Add("kw", new CustomRule(4, (n) => (n == 1) ? 0 : (n == 2) ? 1 : (n == 3) ? 2 : 3));
            // Lithuanian
            rules.Add("lt", new CustomRule(3, (n) => n % 10 == 1 && n % 100 != 11 ? 0 : n % 10 >= 2 && (n % 100 < 10 || n % 100 >= 20) ? 1 : 2));
            // Latvian
            rules.Add("lv", new CustomRule(3, (n) => n % 10 == 1 && n % 100 != 11 ? 0 : n != 0 ? 1 : 2));
            // Macedonian
            rules.Add("mk", new CustomRule(3, (n) => n == 1 || n % 10 == 1 ? 0 : 1));
            // Mandinka
            rules.Add("mnk", new CustomRule(3, (n) => n == 0 ? 0 : n == 1 ? 1 : 2));
            // Maltese
            rules.Add("mt", new CustomRule(4, (n) => n == 1 ? 0 : n == 0 || (n % 100 > 1 && n % 100 < 11) ? 1 : (n % 100 > 10 && n % 100 < 20) ? 2 : 3));
            // Polish
            rules.Add("pl", new CustomRule(3, (n) => n == 1 ? 0 : n % 10 >= 2 && n % 10 <= 4 && (n % 100 < 10 || n % 100 >= 20) ? 1 : 2));
            // Romanian
            rules.Add("ro", new CustomRule(3, (n) => n == 1 ? 0 : (n == 0 || (n % 100 > 0 && n % 100 < 20)) ? 1 : 2));
            // Slovenian
            rules.Add("sl", new CustomRule(4, (n) => n % 100 == 1 ? 1 : n % 100 == 2 ? 2 : n % 100 == 3 || n % 100 == 4 ? 3 : 0));

            return rules;
        }

        private static char[] buffer = new char[6];
        private static Dictionary<string, PluralRule> rules = BuildRules();
        private static Dictionary<int, string> cachedKeys = new Dictionary<int, string>(capacity: 16);

        private static int GetHashCode(char[] buffer, int size)
        {
            unchecked
            {
                int hash = 17;
                for (int i = 0; i < size; ++i)
                {
                    hash = hash * 97 + buffer[i].GetHashCode();
                }

                return hash;
            }
        }

        private static bool TryFindRule(string lang, out PluralRule rule)
        {
            const char zero = (char)0;

            if (rules.TryGetValue(lang, out rule))
            {
                return true;
            }

            for (int i = 0; i < buffer.Length; ++i)
            {
                buffer[i] = zero;
            }

            var size = 0;
            while (size < buffer.Length && size < lang.Length && lang[size] != '-' && lang[size] != '_')
            {
                buffer[size] = lang[size];
                size++;
            }

            var hash = GetHashCode(buffer, size);
            if (!cachedKeys.TryGetValue(hash, out var key))
            {
                key = new string(buffer, 0, size);
                cachedKeys.Add(hash, key);
            }

            if (rules.TryGetValue(key, out rule))
            {
                return true;
            }

            return false;
        }

        public static int GetForm(string lang, long value)
        {
            if (TryFindRule(lang, out var rule))
            {
                return rule.GetForm(value);
            }

            return 0;
        }

        public static int GetNumberOfForms(string lang)
        {
            if (TryFindRule(lang, out var rule))
            {
                return rule.FormsCount;
            }

            return 1;
        }

        public static void AddCustomRule(string lang, PluralRule rule)
        {
            rules.Add(lang, rule);
        }
    }
}
