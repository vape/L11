using UnityEngine;

namespace L11
{
    [CreateAssetMenu(fileName = "Locale", menuName = "L11/Locale")]
    public class LocalePreset : ScriptableObject
    {
        public string Id;
        public string[] Languages;
        public string[] Cultures;
        public string DefaultCulture;

        public TermDictionary PrimaryTerms = new TermDictionary();
        public LocalizableAssetDictionary PrimaryAssets = new LocalizableAssetDictionary();

        public TermDictionaryObject[] ExternalTermDictionaries = new TermDictionaryObject[0];
        public LocalizableAssetDictionaryObject[] ExternalAssetDictionaries = new LocalizableAssetDictionaryObject[0];

        public bool HasLanguage(string language)
        {
            for (int i = 0; i < Languages.Length; ++i)
            {
                if (Languages[i] == language)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasCulture(string culture)
        {
            for (int i = 0; i < Cultures.Length; ++i)
            {
                if (Cultures[i] == culture)
                {
                    return true;
                }
            }

            return false;
        }

        public bool TryFindTerm(string key, out Term term)
        {
            if (PrimaryTerms.TryGetValue(key, out term))
            {
                return true;
            }

            for (int i = 0; i < ExternalTermDictionaries.Length; ++i)
            {
                if (ExternalTermDictionaries[i].Terms.TryGetValue(key, out term))
                {
                    return true;
                }
            }

            return false;
        }

        public bool TryFindAsset(string key, out LocalizableAsset asset)
        {
            if (PrimaryAssets.TryGetValue(key, out asset))
            {
                return true;
            }

            for (int i = 0; i < ExternalAssetDictionaries.Length; ++i)
            {
                if (ExternalAssetDictionaries[i].Assets.TryGetValue(key, out asset))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
