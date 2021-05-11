using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StandWorld.Controllers
{
    public class InfoController : MonoBehaviour
    {
        public TMP_Text title;
        public TMP_Text desc;

        void Awake() {
            title = GetComponentsInChildren<TMP_Text>()[0];
            desc = GetComponentsInChildren<TMP_Text>()[1];
            Reset();
        }

        public void Reset() {
            title.text = "Что такое Lorem Ipsum?";
            desc.text = "Lorem Ipsum - это текст-\"рыба\", часто используемый в печати и вэб-дизайне. Lorem Ipsum является стандартной \"рыбой\" для текстов на латинице с начала XVI века.";
        }
    }
}