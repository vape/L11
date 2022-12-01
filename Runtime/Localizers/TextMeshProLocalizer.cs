#if USING_TEXT_MESH_PRO

using TMPro;
using UnityEngine;

namespace L11.Localizers
{
    [RequireComponent(typeof(TMP_Text))]
    [LocalizerUseKeysFrom(LocalizerKeySource.Terms)]
    public class TextMeshProLocalizer : BaseLocalizer
    {
        [SerializeField]
        private Term fallback;

        private TMP_Text text;

        protected TMP_Text GetText()
        {
            if (text == null)
            {
                text = GetComponent<TMP_Text>();
            }

            return text;
        }

        protected override void Localize()
        {
            if (string.IsNullOrEmpty(key) || !Localization.TryFindTerm(key, out var term))
            {
                term = fallback;
            }

            GetText().text = term.Value;
        }
    }
}

#endif