using UnityEngine;

namespace L11.Localizers
{
    public abstract class BaseLocalizer : MonoBehaviour, ILocaleChangedHandler
    {
        [SerializeField]
        protected string key;

        protected virtual void OnEnable()
        {
            Localize();
        }

        public void OnLocaleChanged(Locale locale)
        {
            Localize();
        }

        protected abstract void Localize();
    }
}
