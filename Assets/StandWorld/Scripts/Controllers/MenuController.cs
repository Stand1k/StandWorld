using System.Collections.Generic;
using ProjectPorcupine.Localization;
using StandWorld.Definitions;
using StandWorld.Helpers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StandWorld.Controllers
{
    public struct MenuOrderTab
    {
        public GameObject go;

        public MenuOrderTab(GameObject go)
        {
            this.go = go;
        }
    }

    public struct MenuOrderTabLink
    {
        public GameObject go;
        public Image image;

        public MenuOrderTabLink(GameObject go, Image image)
        {
            this.go = go;
            this.image = image;
        }
    }

    public struct MenuOrderButton
    {
        public GameObject go;
        public Button button;
        public TMP_Text text;
        public Image image;

        public MenuOrderButton(GameObject go, Button button, TMP_Text text, Image image)
        {
            this.go = go;
            this.button = button;
            this.text = text;
            this.image = image;
        }
    }


    public class MenuController : MonoBehaviour
    {
        public InfoController info;
        public Transform parent;
        public Transform parentMenu;
        public Transform parentContent;
        public MenuOrderButton[] buttons;
        public MenuOrderTab[] tabs;
        public Color activeColor;
        public Color defaultColor;
        public int current = -1;
        public MenuOrderDef currentOrder;
        public Dictionary<string, MenuOrderTabLink> links = new Dictionary<string, MenuOrderTabLink>();
        public Dictionary<KeyCode, int> tabShortcuts = new Dictionary<KeyCode, int>();
        public Dictionary<MenuOrderDef, KeyCode> ordersShortcuts = new Dictionary<MenuOrderDef, KeyCode>();

        void Start()
        {
            int tabCount = 6;
            buttons = new MenuOrderButton[tabCount];
            tabs = new MenuOrderTab[tabCount];
            
            AddTab(LocalizationTable.GetLocalization("button_orders"), 0, KeyCode.A);
            AddTab(LocalizationTable.GetLocalization("button_zones"), 1, KeyCode.S);
            AddTab(LocalizationTable.GetLocalization("button_building"), 2, KeyCode.D);
            AddTab(LocalizationTable.GetLocalization("button_production"), 3);
            AddTab(LocalizationTable.GetLocalization("button_magic"), 4);
            AddTab(LocalizationTable.GetLocalization("button_fight"), 5);
            currentOrder = null;
            Reset();
        }

        public void ClearSelection()
        {
            foreach (MenuOrderButton btn in buttons)
            {
                btn.image.color = defaultColor;
            }

            foreach (MenuOrderTab tab in tabs)
            {
                tab.go.SetActive(false);
            }

            info.Reset();
        }

        public void ClearOrders()
        {
            foreach (MenuOrderTabLink btn in links.Values)
            {
                btn.image.color = defaultColor;
            }
        }

        public void Reset()
        {
            currentOrder = null;
            ClearOrders();
            current = -1;
            ClearSelection();
        }

        public void AddTab(string name, int id, KeyCode key = KeyCode.Escape)
        {
            TMP_Text text;
            Image image;
            Button button;
            
            GameObject go = Instantiate(Res.prefabs["order_panel"]);
            go.transform.SetParent(parent);
            go.name = "OrderTab: " + name;
            tabs[id] = new MenuOrderTab(go);

            List<MenuOrderDef> orders;

            if (id == 0)
            {
                orders = new List<MenuOrderDef>(Defs.orders.Values);
            }
            else if (id == 2)
            {
                orders = new List<MenuOrderDef>(Defs.buildingOrders.Values);
            }
            else
            {
                orders = new List<MenuOrderDef>();
            }

            foreach (MenuOrderDef order in orders)
            {/*
                GameObject _go = Instantiate(Res.prefabs["button_order"]);
                _go.transform.SetParent(go.transform);*/
                
                GameObject goInstantiate = Instantiate(Res.prefabs["ButtonInstance"]);
                goInstantiate.transform.SetParent(parentContent);

                /*_go.name = "OrderButton: " + order.uId;
                text = _go.GetComponentInChildren<TMP_Text>();
                text.text = $"({order.keyCode.ToString()})";
                _go.GetComponentsInChildren<Image>()[1].sprite = order.sprite;
                button = _go.GetComponentInChildren<Button>();
                image = _go.GetComponentsInChildren<Image>()[0]*/;

                goInstantiate.name = "OrderButton: " + order.uId;
                text = goInstantiate.GetComponentInChildren<TMP_Text>();
                text.text = $"({order.keyCode.ToString()})";
                goInstantiate.GetComponentsInChildren<Image>()[1].sprite = order.sprite;
                button = goInstantiate.GetComponentInChildren<Button>();
                image = goInstantiate.GetComponentsInChildren<Image>()[0];
                
                if (order.keyCode != KeyCode.Escape)
                {
                    ordersShortcuts.Add(order, order.keyCode);
                }

                //MenuOrderTabLink orderLink = new MenuOrderTabLink(_go, image);
                MenuOrderTabLink orderLink = new MenuOrderTabLink(goInstantiate, image);
                button.onClick.AddListener(
                    () => ClickOrder(order)
                );
                links.Add(order.uId, orderLink);
            }

            var itemHeight = 95f;
            var totalItems = parentContent.childCount;
            var newContentHeight = itemHeight * totalItems + parentContent.GetComponent<GridLayoutGroup>().spacing.y * totalItems;
            var t = parentContent.GetComponent<RectTransform>();
            t.sizeDelta = new Vector2(t.sizeDelta.x,newContentHeight);
            //t.anchoredPosition = new Vector3(t.anchoredPosition.x,newContentHeight * 0.5f);

            go = Instantiate(Res.prefabs["button_player_panel"]);
            go.transform.SetParent(parentMenu);
            go.name = "Button: " + name;
            text = go.GetComponentInChildren<TMP_Text>();
            text.text = name;
            image = go.GetComponentInChildren<Image>();
            button = go.GetComponentInChildren<Button>();

            if (key != KeyCode.Escape)
            {
                tabShortcuts.Add(key, id);
                text.text += $"({key.ToString()})";
            }

            button.onClick.AddListener(
                () => ClickTab(id)
            );
            buttons[id] = new MenuOrderButton(go, button, text, image);
        }

        public void ClickOrder(MenuOrderDef order)
        {
            ClearOrders();
            if (currentOrder != order)
            {
                currentOrder = order;
                links[order.uId].image.color = activeColor;
                info.title.text = LocalizationTable.GetLocalization(order.uId + "_title");
                info.desc.text = LocalizationTable.GetLocalization(order.uId + "_desc");
            }
            else
            {
                currentOrder = null;
            }
        }

        public void ClickTab(int id)
        {
            if (current != id)
            {
                ClearSelection();
                current = id;
                buttons[current].image.color = activeColor;
                tabs[current].go.SetActive(true);
            }
            else
            {
                Reset();
            }
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                Reset();
            }

            foreach (KeyValuePair<KeyCode, int> kv in tabShortcuts)
            {
                if (Input.GetKeyDown(kv.Key))
                {
                    ClickTab(kv.Value);
                }
            }

            if (current >= 0)
            {
                foreach (KeyValuePair<MenuOrderDef, KeyCode> kv in ordersShortcuts)
                {
                    if (Input.GetKeyDown(kv.Value))
                    {
                        ClickOrder(kv.Key);
                    }
                }
            }
        }
    }
}