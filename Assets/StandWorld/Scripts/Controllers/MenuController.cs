using System.Collections.Generic;
using Microsoft.Win32;
using StandWorld.Definitions;
using StandWorld.Helpers;
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
        public Text text;
        public Image image;

        public MenuOrderButton(GameObject go, Button button, Text text, Image image)
        {
            this.go = go;
            this.button = button;
            this.text = text;
            this.image = image;
        }
    }


    public class MenuController : MonoBehaviour
    {
        [SerializeField] private InfoController info;
        [SerializeField] private Transform parent;
        [SerializeField] private Transform parentMenu;
        [SerializeField] private MenuOrderButton[] buttons;
        [SerializeField] private MenuOrderTab[] tabs;
        [SerializeField] private Color activeColor;
        [SerializeField] private Color defaultColor;
        [SerializeField] private int current = -1;

        public MenuOrderDef currentOrder;
        public Dictionary<string, MenuOrderTabLink> links = new Dictionary<string, MenuOrderTabLink>();
        public Dictionary<KeyCode, int> tabShortcuts = new Dictionary<KeyCode, int>();
        public Dictionary<KeyCode, MenuOrderDef> ordersShortcuts = new Dictionary<KeyCode, MenuOrderDef>();

        void Start()
        {
            int tabCount = 6;
            buttons = new MenuOrderButton[tabCount];
            tabs = new MenuOrderTab[tabCount];

            AddTab("Накази", 0, KeyCode.A);
            AddTab("Зони", 1, KeyCode.S);
            AddTab("Будівлі", 2, KeyCode.D);
            AddTab("Виробництво", 3);
            AddTab("Магія", 4);
            AddTab("Бій", 5);
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
            Text text;
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
            {
                GameObject _go = Instantiate(Res.prefabs["button_order"]);
                _go.transform.SetParent(go.transform);
                _go.name = "OrderButton: " + order.name;
                text = _go.GetComponentInChildren<Text>();
                text.text = $"({order.keyCode.ToString()})";
                _go.GetComponentsInChildren<Image>()[1].sprite = order.sprite;
                button = _go.GetComponentInChildren<Button>();
                image = _go.GetComponentsInChildren<Image>()[0];

                if (order.keyCode != KeyCode.Escape)
                {
                    ordersShortcuts.Add(order.keyCode, order);
                }

                MenuOrderTabLink orderLink = new MenuOrderTabLink(_go, image);
                button.onClick.AddListener(
                    () => ClickOrder(order)
                );
                links.Add(order.uId, orderLink);
            }

            go = Instantiate(Res.prefabs["button_player_panel"]);
            go.transform.SetParent(parentMenu);
            go.name = "Button: " + name;
            text = go.GetComponentInChildren<Text>();
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
                info.title.text = order.name;
                info.desc.text = order.shortDesc;
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

            foreach (KeyValuePair<KeyCode,int> kv in tabShortcuts)
            {
                if (Input.GetKeyDown(kv.Key))
                {
                    ClickTab(kv.Value);
                }
            }

            if (current >= 0)
            {
                foreach (KeyValuePair<KeyCode,MenuOrderDef> kv in ordersShortcuts)
                {
                    if (Input.GetKeyDown(kv.Key))
                    {
                        ClickOrder(kv.Value);
                    }
                }
            }
        }
    }
}