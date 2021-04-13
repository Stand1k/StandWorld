using System.Collections.Generic;
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
        [SerializeField] private  Color activeColor;
        [SerializeField] private Color defaultColor;
        [SerializeField] private int current = -1;
        [SerializeField] private MenuOrderDef currentOrder;
        [SerializeField] private List<MenuOrderTabLink> links;

        void Start()
        {
            links = new List<MenuOrderTabLink>();
            int tabCount = 6;
            buttons = new MenuOrderButton[tabCount];
            tabs = new MenuOrderTab[tabCount];

            AddTab("Накази", 0);
            AddTab("Зони", 1);
            AddTab("Будівлі", 2);
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
            foreach (MenuOrderTabLink btn in links)
            {
                btn.image.color = defaultColor;
            }
        }

        public void Reset()
        {
            if (currentOrder == null)
            {
                current = -1;
                ClearSelection();
            }
            else
            {
                currentOrder = null;
                ClearOrders();
            }
        }

        public void AddTab(string name, int id)
        {
            Text text;
            Image image;
            Button button;

            GameObject go = Instantiate(Res.prefabs["order_panel"]);
            go.transform.SetParent(parent);
            go.name = "OrderTab: " + name;
            tabs[id] = new MenuOrderTab(go);
            
            List<MenuOrderDef> orders = new List<MenuOrderDef>();
            
            if (id == 0)
            {
                orders = new List<MenuOrderDef>(Defs.orders.Values);
            }

            foreach (MenuOrderDef order in orders)
            {
                GameObject _go = Instantiate(Res.prefabs["button_order"]);
                _go.transform.SetParent(go.transform);
                _go.name = "OrderButton: " + order.name;
                text = _go.GetComponentInChildren<Text>();
                text.text = "Alt+F4"; 
                _go.GetComponentsInChildren<Image>()[1].sprite = order.sprite;
                button = _go.GetComponentInChildren<Button>();
                image = _go.GetComponentsInChildren<Image>()[0];
                
                MenuOrderTabLink orderLink = new MenuOrderTabLink(_go, image);
                button.onClick.AddListener(
                    delegate
                    {
                        ClearOrders();
                        currentOrder = order;
                        orderLink.image.color = activeColor;
                        info.title.text = order.name;
                        info.desc.text = order.shortDesc;
                    }
                );
                links.Add(orderLink);
            }

            go = Instantiate(Res.prefabs["button_player_panel"]);
            go.transform.SetParent(parentMenu);
            go.name = "Button: " + name;
            text = go.GetComponentInChildren<Text>();
            text.text = name;
            image = go.GetComponentInChildren<Image>();
            button = go.GetComponentInChildren<Button>();
            button.onClick.AddListener(
                delegate
                {
                    ClearSelection();
                    current = id;
                    buttons[current].image.color = activeColor;
                    tabs[current].go.SetActive(true);
                }
            );
            buttons[id] = new MenuOrderButton(go, button, text, image);
        }

        public void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape) || Input.GetMouseButtonDown(1))
            {
                Reset();
            }
        }
    }
}