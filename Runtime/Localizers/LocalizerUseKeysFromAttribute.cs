using System;

namespace L11.Localizers
{
    public enum LocalizerKeySource
    {
        Terms,
        Assets
    }

    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class LocalizerUseKeysFromAttribute : Attribute
    {
        public readonly LocalizerKeySource Source;

        public LocalizerUseKeysFromAttribute(LocalizerKeySource source)
        {
            Source = source;
        }
    }
}
