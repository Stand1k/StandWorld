using StandWorld.Definitions;
using StandWorld.Entities;
using TMPro;
using UnityEngine;

namespace StandWorld.Controllers
{
    [RequireComponent(typeof(TextMeshPro))]
    public class LabelComponent : MonoBehaviour
    {
        private Stackable _stackable;
        private RectTransform _rt;
        private TextMeshPro _tm;

        private void Awake()
        {
            transform.position = new Vector3(0, 0, LayerUtils.Height(Layer.Stackable));
            _tm = gameObject.GetComponent<TextMeshPro>();
            _rt = (RectTransform) transform;
            _rt.offsetMin = Vector2.zero;
            _rt.offsetMax = Vector2.one;
            _tm.alignment = TextAlignmentOptions.MidlineGeoAligned;
            _tm.fontStyle = FontStyles.Bold;
            _tm.fontSize = 4;
        }

        public void SetStackable(Stackable stackable)
        {
            _stackable = stackable;
            _rt.offsetMin = new Vector2(_stackable.position.x, _stackable.position.y);
            _rt.offsetMax = new Vector2(_stackable.position.x + 1, _stackable.position.y + 1);
            _tm.text = _stackable.inventory.count.ToString();

            /*if (!_stackable.bucket.IsVisible())
            {
                gameObject.SetActive(false);
            }*/
        }

        private void Update()
        {
            if (_stackable != null)
            {
                _tm.text = _stackable.inventory.count.ToString();

                /*if (gameObject.activeInHierarchy && !_stackable.bucket.IsVisible())
                {
                    gameObject.SetActive(false);
                }
                else if (!gameObject.activeInHierarchy && _stackable.bucket.IsVisible())
                {
                    gameObject.SetActive(true);
                }*/
            }
        }
    }
}