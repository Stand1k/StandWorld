using System.Collections.Generic;
using StandWorld.Helpers;
using UnityEngine;
using Vector2 = System.Numerics.Vector2;

namespace StandWorld.UI
{
    public abstract class Window
    {
        public static List<Window> windows = new List<Window>();

        public string title { get; protected set; }

        public string titleTab
        {
            get
            {
                if (!_hasTabs)
                {
                    return title;
                }

                return tabs[activeTab] + " : " + title;
            }
        }

        public bool draggable { get; protected set; }
        public bool resizeable { get; protected set; }
        public bool closeButton { get; protected set; }
        public Vector2 initialSize { get; protected set; }
        public Vector2 initialPosition { get; protected set; }
        public Vector2 size { get; protected set; }
        public Vector2 position { get; protected set; }
        public RectOffset padding { get; protected set; }
        public Rect rect { get; protected set; }
        public Rect contentRect { get; protected set; }
        public List<string> tabs { get; protected set; }
        public int activeTab { get; protected set; }
        public float headerSize { get; protected set; }

        public WindowVerticalGrid vGrid { get; protected set; }
        protected bool _hasTitle;
        protected bool _hasTabs;
        protected bool _show = true;

        private int _id;

        public Window()
        {
            windows.Add(this);
            _id = windows.Count;

            _hasTitle = (title != "");
            closeButton = true;
            draggable = false;
            resizeable = false;
            _show = true;
            tabs = new List<string>();
            padding = new RectOffset(20, 20, 20, 20);
            initialSize = new Vector2(700f, 700f);
            rect = GetRectAtCenter();
            size = initialSize;
        }

        public void Show()
        {
            _show = true;
        }

        public void Hide()
        {
            _show = false;
        }

        protected void AddTab(string name)
        {
            if (_hasTabs == false)
            {
                _hasTabs = true;
                activeTab = 0;
            }

            tabs.Add(name);
        }

        public void SetActiveTab(int id)
        {
            activeTab = id;
        }

        protected void SetTitle(string title)
        {
            _hasTitle = true;
            this.title = title;
        }

        public Rect GetRectAtCenter()
        {
            return new Rect(
                (Screen.width - initialSize.X) / 2f,
                (Screen.height - initialSize.X) / 2f,
                initialSize.X,
                initialSize.Y
            );
        }

        public void UpdateHeight(float height)
        {
            initialSize = new Vector2(initialSize.X, height);
            rect = GetRectAtCenter();
        }

        public virtual void Header()
        {
            if (_hasTabs)
            {
                vGrid.Tabs(tabs, activeTab);
            }
        }

        public abstract void Content();

        public virtual void DoMyWindow(int windowID)
        {
            vGrid = new WindowVerticalGrid();
            vGrid.Begin(rect.size, this);
            Header();
            Content();
            vGrid.End();
        }

        public void OnGUI()
        {
            if (!_show)
            {
                return;
            }

            GUI.skin = Res.defaultGUI;
            rect = GUI.Window(_id, rect, DoMyWindow, titleTab);
        }
    }
}