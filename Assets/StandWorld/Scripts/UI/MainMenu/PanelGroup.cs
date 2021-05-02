using System;
using UnityEngine;

namespace StandWorld.UI.MainMenu
{
    public class PanelGroup : MonoBehaviour
    {
        public GameObject[] panels;
        public TabGroup tabGroup;
        public int panelIndex;

        private void Awake()
        {
            ShowCurrentPanel();
        }

        private void ShowCurrentPanel()
        {
            for (int i = 0; i < panels.Length; i++)
            {
                if (i == panelIndex)
                {
                    panels[i].gameObject.SetActive(true);
                }
                else
                {
                    panels[i].gameObject.SetActive(false);
                }
            }
        }

        public void SetPageIndex(int index)
        {
            panelIndex = index;
            ShowCurrentPanel();
        }
    }
}
