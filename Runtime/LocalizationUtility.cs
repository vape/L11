using System;
using System.Globalization;

namespace L11
{
    public struct L11FormatArgument : IFormattable
    {
        private const char Separator = '|';

        public static implicit operator L11FormatArgument(string str)
        {
            return new L11FormatArgument(str);
        }

        public static implicit operator L11FormatArgument(uint value)
        {
            return new L11FormatArgument(value);
        }

        public static implicit operator L11FormatArgument(int value)
        {
            return new L11FormatArgument(value);
        }

        public static implicit operator L11FormatArgument(ulong value)
        {
            return new L11FormatArgument((long)value);
        }

        public static implicit operator L11FormatArgument(long value)
        {
            return new L11FormatArgument(value);
        }

        public static implicit operator L11FormatArgument(ushort value)
        {
            return new L11FormatArgument(value);
        }

        public static implicit operator L11FormatArgument(short value)
        {
            return new L11FormatArgument(value);
        }

        public static implicit operator L11FormatArgument(byte value)
        {
            return new L11FormatArgument(value);
        }

        public string String;
        public long Number;

        public L11FormatArgument(string str)
        {
            String = str;
            Number = 0;
        }

        public L11FormatArgument(long num)
        {
            String = null;
            Number = num;
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            var provider = formatProvider as CultureInfo ?? Localization.Locale.Culture;

            if (format == null)
            {
                if (String != null)
                {
                    return String;
                }
                else
                {
                    return Number.ToString();
                }
            }

            if (format.IndexOf(Separator) == -1)
            {
                // if its a number and not string, fallback to number format
                if (String == null)
                {
                    return Number.ToString(format, provider);
                }

                return format;
            }

            var variants = format.Split(Separator);
            var form = Plural.GetForm(provider.Name, Number);

            return variants[Math.Min(variants.Length - 1, form)];
        }
    }

    public static class LocalizationUtility
    {
        private static object[] AsObjectsArray(this L11FormatArgument[] arguments)
        {
            var objects = new object[arguments.Length];

            for (int i = 0; i < arguments.Length; ++i)
            {
                objects[i] = arguments[i];
            }

            return objects;
        }

        public static string Localize(this string key, string fallback = default, params L11FormatArgument[] arguments)
        {
            return String.Format(Localize(key, fallback), AsObjectsArray(arguments));
        }

        public static string Localize(this string key, params L11FormatArgument[] arguments)
        {
            return String.Format(Localize(key, fallback: default), AsObjectsArray(arguments));
        }

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
