using UnityEngine;
using UnityEngine.UI;

namespace StandWorld.UI.MainMenu
{
    [RequireComponent(typeof(CanvasGroup))]
    [AddComponentMenu("UI/Blur Panel")]
    public class BlurPanel : Image
    {
        public float time = 0.5f;
        public float delay = 0;

        private CanvasGroup _canvasGroup;

        protected override void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            if (Application.isPlaying)
            {
                color = Color.black * 0.1f;
                material.SetFloat("_Size", 0);
                _canvasGroup.alpha = 0f;
                LeanTween.value(gameObject, UpdateBlur, 0, 3, time).setDelay(delay);
            }
        }

        void UpdateBlur(float value)
        {
            material.SetFloat("_Size", value);
            _canvasGroup.alpha = value;
        }
    }
}
