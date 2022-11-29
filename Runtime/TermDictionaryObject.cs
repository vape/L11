using System;
using UnityEngine;

namespace L11
{
    [Serializable]
    [CreateAssetMenu(fileName = "Term Dictionary", menuName = "L11/Term Dictionary")]
    public class TermDictionaryObject : ScriptableObject
    {
        [SerializeField]
        public TermDictionary Terms;

        public static implicit operator TermDictionary(TermDictionaryObject obj)
        {
            return obj.Terms;
        }
    }
}
