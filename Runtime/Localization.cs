using System;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;

[assembly: InternalsVisibleTo("L11.Editor")]

namespace L11
{
    public delegate void LocaleChangedDelegate(Locale locale);

    public interface ILocaleChangedHandler
    {
        void OnLocaleChanged(Locale locale);
    }

    public interface ILocaleChangedInEditorHandler
    {
        void OnLocaleChangedInEditor(Locale locale);
    }

    public class Localization
    {
        public static event LocaleChangedDelegate LocaleChanged;

        public static Locale Locale
        { get { return activeLocale; } }

        private static Locale activeLocale;
        private static Locale defaultLocale;

        static Localization()
        {
            Init();
        }

        internal static void ConfigChanged()
        {
            Init();
        }

        private static void Init()
        {
            var config = Config.Locate();

            defaultLocale = CreateDefaultLocale(config) ?? CreateFallbackLocale();
            SetLocale(CreateSystemLocale(config) ?? defaultLocale);
        }

        public static bool HasLocale(string id)
        {
            var config = Config.Locate();

            for (int i = 0; i < config.Locales.Length; i++)
            {
                if (config.Locales[i].Id == id)
                {
                    return true;
                }
            }

            return false;
        }

        public static void SetLocale(string id)
        {
            var config = Config.Locate();

            for (int i = 0; i < config.Locales.Length; i++)
            {
                if (config.Locales[i].Id == id)
                {
                    SetLocale(new Locale(config.Locales[i]));
                    return;
                }
            }
        }

        private static void SetLocale(Locale locale)
        {
            activeLocale = locale;

            LocaleChanged?.Invoke(activeLocale);

            if (Application.isPlaying)
            {
                foreach (var behaviour in UnityEngine.Object.FindObjectsOfType<MonoBehaviour>().OfType<ILocaleChangedHandler>())
                {
                    behaviour.OnLocaleChanged(activeLocale);
                }
            }
            else
            {
                foreach (var behaviour in UnityEngine.Object.FindObjectsOfType<MonoBehaviour>().OfType<ILocaleChangedInEditorHandler>())
                {
                    behaviour.OnLocaleChangedInEditor(activeLocale);
                }
            }
        }

        public static bool TryFindTerm(string key, out Term result)
        {
            if (activeLocale.Preset.TryFindTerm(key, out result))
            {
                return true;
            }

            if (defaultLocale.Preset.TryFindTerm(key, out result))
            {
                return true;
            }

            return false;
        }

        public static bool TryFindAsset(string key, out LocalizableAsset asset)
        {
            if (activeLocale.Preset.TryFindAsset(key, out asset))
            {
                return true;
            }

            if (defaultLocale.Preset.TryFindAsset(key, out asset))
            {
                return true;
            }

            return false;
        }

        public static string GetString(string key, string fallback = default)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (TryFindTerm(key, out var term))
            {
                return term.Value;
            }

            return fallback == default ? key : fallback;
        }

        public static UnityEngine.Object GetObject(string key, UnityEngine.Object fallback = default)
        {
            return GetObjectInternal(key, fallback);
        }

        public static TObject GetObject<TObject>(string key, TObject fallback = default)
            where TObject: UnityEngine.Object
        {
            return GetObjectInternal(key, fallback) as TObject;
        }

        private static UnityEngine.Object GetObjectInternal(string key, UnityEngine.Object fallback = default)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            if (TryFindAsset(key, out var asset))
            {
                return asset.Value;
            }

            return fallback;
        }

        private static Locale CreateSystemLocale(Config config)
        {
            var preset = TryFindPreset(config, CultureInfo.CurrentCulture, out var presetCulture);
            if (preset != null)
            {
                return new Locale(preset, presetCulture);
            }

            return null;
        }

        private static Locale CreateDefaultLocale(Config config)
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

        private static LocalePreset TryFindPreset(Config config, CultureInfo culture, out CultureInfo presetCulture)
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
