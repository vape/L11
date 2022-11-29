using System;
using UnityEngine;

namespace L11
{
    [Serializable]
    [CreateAssetMenu(fileName = "Assets Dictionary", menuName = "L11/Assets Dictionary")]
    public class LocalizableAssetDictionaryObject : ScriptableObject
    {
        [SerializeField]
        public LocalizableAssetDictionary Assets;

        public static implicit operator LocalizableAssetDictionary(LocalizableAssetDictionaryObject obj)
        {
            return obj.Assets;
        }
    }
}
