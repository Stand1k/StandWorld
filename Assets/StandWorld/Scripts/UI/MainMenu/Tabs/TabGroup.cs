using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace StandWorld.UI.MainMenu
{
    public class TabGroup : MonoBehaviour
    {
        [Header("Tab Color")]
        public Color tabActive;
        public Color tabExit;
        [Header("Text Color")]
        public Color textActive;
        public Color textExit;
        
        public TabButton selectedTab;
        public int DeselectIndex;
        public PanelGroup panelGroup;   
        
        public List<TabButton> tabButtons;
        [HideInInspector] public List<GameObject> objectsToSwap;

        public void Subscribe(TabButton button)
        {
            if (tabButtons == null)
            {
                tabButtons = new List<TabButton>();
            }

            tabButtons.Add(button);
        }
        
        public void OnTabEnter(TabButton button)
        {   
            ResetTabs();
            if (selectedTab == null || button != selectedTab)
            {
                Color temp1 = tabActive;
                temp1.a = 0.9f;
                button.background.color = temp1;
                GameObject gameObject = button.gameObject.transform.GetChild(0).gameObject;
                gameObject.GetComponent<TextMeshProUGUI>().color = textActive;
            }
        }

        public void OnTabExit(TabButton button)
        {
            ResetTabs();
        }

        public void OnTabSelected(TabButton button)
        {
            if (selectedTab != null)
            {
                selectedTab.Deselect();
            }
            
            selectedTab = button;
            
            selectedTab.Select();
            
            ResetTabs();
            button.background.color = tabActive;
            GameObject gameObject = button.gameObject.transform.GetChild(0).gameObject;
            gameObject.GetComponent<TextMeshProUGUI>().color = textActive;
            int index = button.transform.GetSiblingIndex();

            if (panelGroup != null)
            {
                panelGroup.SetPageIndex(selectedTab.transform.GetSiblingIndex(), DeselectIndex);
                DeselectIndex = selectedTab.transform.GetSiblingIndex();
            }
            
            for (int i = 0; i < objectsToSwap.Count; i++)
            {
                if (i == index)
                {
                    objectsToSwap[i].SetActive(true);
                    
                }
                else
                {
                    objectsToSwap[i].SetActive(false);
                }
            }
        }

        public void ResetTabs()
        {
            foreach (TabButton button in tabButtons)
            {
                if (selectedTab != null && button == selectedTab)
                {
                    continue;
                }

                button.background.color = tabExit;
                GameObject gameObject = button.gameObject.transform.GetChild(0).gameObject;
                gameObject.GetComponent<TextMeshProUGUI>().color = textExit;
            }
        }
    }
}
