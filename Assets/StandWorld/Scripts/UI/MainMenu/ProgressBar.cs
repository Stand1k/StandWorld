using System;
using UnityEngine;
using UnityEngine.UI;

namespace StandWorld.UI.MainMenu
{
    [ExecuteInEditMode]
    public class ProgressBar : MonoBehaviour
    {
        public int min;
        public int max;
        public float current;
        public Image mask;
        public Image fill;
        public Color color;

        private void Update()
        {
            GetCurrentFill();
        }

        void GetCurrentFill()
        {
            float currentOffset = current - min;
            float maximumOffset = max - min;
            float fillAmount = currentOffset / maximumOffset;
            mask.fillAmount = fillAmount;

            fill.color = color;
        }
    }
}