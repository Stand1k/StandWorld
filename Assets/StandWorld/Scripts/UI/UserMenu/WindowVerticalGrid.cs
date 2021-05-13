using System.Collections.Generic;
using StandWorld.Helpers;
using UnityEngine;

namespace StandWorld.UI
{
    public class WindowVerticalGrid
    {
        public float width { get; protected set; }
        public float spacing { get; protected set; }
        public int currentCount { get; protected set; }
        public float currentHeight { get; protected set; }
        private Window _window;

        public void Begin(Vector2 size, Window window)
        {
            currentHeight = Res.defaultGUI.window.padding.top;
            currentCount = 0;
            spacing = 6f;
            _window = window;
            width = size.x;
            GUI.BeginGroup(new Rect(0, 0, size.x, size.y));
        }

        public void H1(string text)
        {
            H(text, WindowComponents.titleStyle);
        }

        public void H2(string text)
        {
            H(text, WindowComponents.subTitleStyle);
        }

        public void H(string text, GUIStyle style)
        {
            Rect rect = GetRect(style.CalcHeight(new GUIContent(text), width));
            GUI.color = new Color(1f, 0.98f, 0.94f, 1f);
            GUI.Label(rect, text, style);
            GUI.DrawTexture(
                new Rect(rect.x, rect.y + rect.height, rect.width, 1),
                Res.TextureUnicolor(new Color(1, 1, 1, .5f))
            );
            
            GUI.color = Color.white;
        }

        public void Tabs(List<string> list, int active = 0)
        {
            Rect rect = new Rect(8f, 0, width - 8f, 30f);
            Rect closeRect = new Rect(rect.width - 31f, 0, 31f, 30f - 4f);
            currentHeight += spacing * 2;

            float[] tabsWidths = new float[list.Count];
            for (int i = 0; i < list.Count; i++)
            {
                tabsWidths[i] = WindowComponents.buttonLabelStyle.CalcSize(new GUIContent(list[i])).x + 40;
            }

            Rect[] horizontalGrid = rect.HorizontalGrid(tabsWidths, 5f);

            for (int i = 0; i < list.Count; i++)
            {
                float height = (active == i) ? horizontalGrid[i + 1].height : horizontalGrid[i + 1].height - 4;
                Rect tabRect = horizontalGrid[i + 1].Height(height);
                string style = (active == i) ? "tab" : "tab_off";

                if (GUI.Button(tabRect, list[i], Res.defaultGUI.GetStyle(style)))
                {
                    _window.SetActiveTab(i);
                }
            }

            GUI.color = new Color(0.94f, 0.78f, 0f);
            if (GUI.Button(closeRect, new GUIContent("х"), Res.defaultGUI.GetStyle("tab_close")))
            {
                _window.Hide();
            }

            GUI.color = Color.white;
        }

        public void Paragraph(string text)
        {
            Rect rect = GetRect(WindowComponents.blockTextStyle.CalcHeight(new GUIContent(text), width));
            GUI.Label(rect, new GUIContent(text), WindowComponents.blockTextStyle);
        }

        public void Span(string text)
        {
            Rect rect = GetRect(WindowComponents.labelStyle.CalcHeight(new GUIContent(text), width));
            GUI.Label(rect, text, WindowComponents.labelStyle);
        }

        public Rect GetNewRect(float height)
        {
            return GetRect(height);
        }

        private Rect GetRect(float height)
        {
            Rect r = new Rect(Res.defaultGUI.window.padding.left, currentHeight,
                width - Res.defaultGUI.window.padding.horizontal, height);
            currentHeight += height + spacing;
            currentCount++;
            return r;
        }

        public void End()
        {
            currentHeight += Res.defaultGUI.window.padding.bottom;
            _window.UpdateHeight(currentHeight);
            GUI.EndGroup();
        }
    }
}