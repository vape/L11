using UnityEngine;
using UnityEngine.UI;

namespace L11.Localizers
{
    [RequireComponent(typeof(Image))]
    [LocalizerUseKeysFrom(LocalizerKeySource.Assets)]
    public class ImageLocalizer : BaseLocalizer
    {
        [SerializeField]
        private LocalizableAsset<Sprite> fallback;

        private Image image;

        private Image GetImage()
        {
            if (image == null)
            {
                image = GetComponent<Image>();
            }

            return image;
        }

        protected override void Localize()
        {
            if (string.IsNullOrEmpty(key) || !Localization.TryFindAsset(key, out var asset))
            {
                asset = fallback;
            }

            GetImage().sprite = asset.Value as Sprite;
        }
    }
}
