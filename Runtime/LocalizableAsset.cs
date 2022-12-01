using System;
using UnityEngine;

namespace L11
{
    [Serializable]
    public class LocalizableAsset
    {
        [SerializeField]
        public UnityEngine.Object Value;
    }

    [Serializable]
    public class LocalizableAsset<TAssetType> : LocalizableAsset
        where TAssetType: UnityEngine.Object
    {
        public TAssetType TypedValue
        { get { return Value as TAssetType; } }
    }
}
