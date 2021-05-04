using System;
using DG.Tweening;
using UnityEngine;

namespace StandWorld.UI.MainMenu
{
    public class PanelGroup : MonoBehaviour
    {
        public GameObject[] panels;
        public TabGroup tabGroup;
        public int selectPanelIndex;
        public int deselectPanelIndex;

        private void Awake()
        {
            ShowCurrentPanel();
        }

        private void ShowCurrentPanel()
        {
            if (selectPanelIndex != deselectPanelIndex)
            {
                Sequence sequence = DOTween.Sequence();
                sequence
                    .Append(panels[deselectPanelIndex].gameObject.GetComponent<RectTransform>()
                        .DOAnchorPos(new Vector2(4920, 0), 0.25f).OnComplete(
                            () =>
                            {
                                panels[deselectPanelIndex].gameObject.SetActive(false);
                            }))
                    .Append(panels[selectPanelIndex].gameObject.GetComponent<RectTransform>()
                        .DOAnchorPos(new Vector2(1450, 0), 0.25f).OnStart(
                            () =>
                            {
                                panels[selectPanelIndex].gameObject.SetActive(true);
                            }));
            }
                
        }
        

        public void SetPageIndex(int selectIndex, int deselectIndex)
        {
            selectPanelIndex = selectIndex;
            deselectPanelIndex = deselectIndex;
            ShowCurrentPanel();
        }
    }
}
