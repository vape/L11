using System;
using System.Globalization;

namespace L11
{
    public class Locale
    {
        public CultureInfo Culture
        { get { return culture; } }
        public LocalePreset Preset
        { get { return preset; } }

        private LocalePreset preset;
        private CultureInfo culture;

        public Locale(LocalePreset preset, CultureInfo culture)
        {
            this.preset = preset;
            this.culture = culture;
        }

        public Locale(LocalePreset preset, string culture)
            : this(preset, new CultureInfo(culture))
        { }

        public Locale(LocalePreset preset)
            : this(preset, preset.DefaultCulture)
        { }
    }
}
