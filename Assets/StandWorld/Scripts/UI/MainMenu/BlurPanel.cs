using DG.Tweening;
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
        private int _SizeID;

        protected override void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _SizeID = Shader.PropertyToID("_Size");
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            
            if (Application.isPlaying)
            {
                material.SetFloat(_SizeID, 0);
                color = Color.black * 0.1f;
                _canvasGroup.alpha = 0f;
                _canvasGroup.DOFade(1, time).OnComplete(() =>
                {
                    material.DOFloat(2, _SizeID, time);
                });
            }
        }


    }
}
