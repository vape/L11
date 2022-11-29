namespace L11
{
    public static class LocalizationUtility
    {
        public static string Localize(this string key, string fallback = default)
        {
            return Localization.GetString(key, fallback);
        }

        public static UnityEngine.Object LocalizeAsset(this string key, UnityEngine.Object fallback = default)
        {
            return Localization.GetObject(key, fallback);
        }
    }
}
