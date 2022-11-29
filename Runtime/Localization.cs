using System;
using System.Globalization;
using UnityEngine;

namespace L11
{
    public class Localization
    {
        static Localization()
        {
            Init(Config.Locate());
        }

        public static Locale Locale
        { get { return activeLocale; } }

        private static Locale activeLocale;
        private static Locale defaultLocale;
        private static Config config;

        private static void Init(Config config)
        {
            Localization.config = config;

            defaultLocale = CreateDefaultLocale() ?? CreateFallbackLocale();
            activeLocale = CreateSystemLocale() ?? defaultLocale;
        }

        public static string GetString(string key, string fallback = default)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (activeLocale.Preset.TryFindTerm(key, out var term))
            {
                return term.Value;
            }

            if (defaultLocale.Preset.TryFindTerm(key, out term))
            {
                return term.Value;
            }

            if (fallback == null)
            {
                return key;
            }

            return fallback;
        }

        public static UnityEngine.Object GetAsset(string key, UnityEngine.Object fallback = default)
        {
            return GetAssetInternal(key, fallback);
        }

        public static TAsset GetAsset<TAsset>(string key, TAsset fallback = default)
            where TAsset: UnityEngine.Object
        {
            return GetAssetInternal(key, fallback) as TAsset;
        }

        private static UnityEngine.Object GetAssetInternal(string key, UnityEngine.Object fallback = default)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (activeLocale.Preset.TryFindAsset(key, out var asset))
            {
                return asset.Value;
            }

            if (defaultLocale.Preset.TryFindAsset(key, out asset))
            {
                return asset.Value;
            }

            return fallback;
        }

        private static Locale CreateSystemLocale()
        {
            var preset = TryFindPreset(CultureInfo.CurrentCulture, out var presetCulture);
            if (preset != null)
            {
                return new Locale(preset, presetCulture);
            }

            return null;
        }

        private static Locale CreateDefaultLocale()
        {
            if (config.Locales.Length == 0)
            {
                return null;
            }

            for (int i = 0; i < config.Locales.Length; ++i)
            {
                if (config.Locales[i].Id == config.DefaultLocaleId)
                {
                    return new Locale(config.Locales[i]);
                }
            }

            return new Locale(config.Locales[0]);
        }

        private static Locale CreateFallbackLocale()
        {
            var preset = ScriptableObject.CreateInstance<LocalePreset>();
            preset.Id = "Fallback";
            preset.Languages = new string[] { "en" };
            preset.Cultures = new string[0];
            preset.DefaultCulture = "en-US";

            return new Locale(preset);
        }

        private static LocalePreset TryFindPreset(CultureInfo culture, out CultureInfo presetCulture)
        {
            if (culture == null)
            {
                presetCulture = default;
                return null;
            }

            for (int i = 0; i < config.Locales.Length; ++i)
            {
                if (config.Locales[i].HasCulture(culture.Name))
                {
                    presetCulture = culture;
                    return config.Locales[i];
                }
            }

            for (int i = 0; i < config.Locales.Length; ++i)
            {
                if (config.Locales[i].HasLanguage(culture.TwoLetterISOLanguageName))
                {
                    if (string.IsNullOrEmpty(config.Locales[i].DefaultCulture))
                    {
                        presetCulture = culture;
                    }
                    else
                    {
                        presetCulture = new CultureInfo(config.Locales[i].DefaultCulture);
                    }

                    return config.Locales[i];
                }
            }

            presetCulture = default;
            return null;
        }
    }
}
