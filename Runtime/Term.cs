using System;
using UnityEngine;

namespace L11
{
    [Serializable]
    public class Term
    {
        [SerializeField]
        [Multiline]
        public string Value;
        [SerializeField]
        public string Context;
    }
}
