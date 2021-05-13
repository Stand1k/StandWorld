using System.Collections.Generic;
using StandWorld.Characters;
using StandWorld.Helpers;
using UnityEngine;

namespace StandWorld.UI
{
    public static class WindowComponents
    {
        public static GUIStyle emptyStyle;
        public static GUIStyle labelStyle;
        public static GUIStyle titleStyle;
        public static GUIStyle subTitleStyle;

        public static GUIStyle buttonLabelStyle;
        public static GUIStyle vitalLabelStyle;
        public static GUIStyle blockTextStyle;
        public static GUIStyle windowStyle;

        public static Dictionary<int, Dictionary<Rect, Rect>> subRectCache;

        public static void LoadComponents()
        {
            subRectCache = new Dictionary<int, Dictionary<Rect, Rect>>();
            emptyStyle = new GUIStyle("label");
            emptyStyle.normal.textColor = new Color(1f, 0.98f, 0.94f, 1f);

            labelStyle = new GUIStyle("label");
            labelStyle.alignment = TextAnchor.MiddleLeft;
            labelStyle.fontStyle = FontStyle.Normal;
            labelStyle.fontSize = 18;
            labelStyle.normal.textColor = new Color(1f, 0.98f, 0.94f, 1f);

            titleStyle = new GUIStyle("label");
            titleStyle.fontSize = 16;
            titleStyle.fontStyle = FontStyle.Bold;
            titleStyle.alignment = TextAnchor.MiddleCenter;
            titleStyle.normal.textColor = new Color(1f, 0.98f, 0.94f, 1f);

            subTitleStyle = new GUIStyle("label");
            subTitleStyle.fontSize = 24;
            subTitleStyle.fontStyle = FontStyle.Bold;
            subTitleStyle.alignment = TextAnchor.MiddleLeft;
            subTitleStyle.normal.textColor = new Color(1f, 0.98f, 0.94f, 1f);

            buttonLabelStyle = new GUIStyle("label");
            buttonLabelStyle.fontStyle = FontStyle.Bold;
            buttonLabelStyle.fontSize = 14;
            buttonLabelStyle.padding = new RectOffset(2, 2, 2, 2);
            buttonLabelStyle.alignment = TextAnchor.MiddleCenter;
            buttonLabelStyle.normal.textColor = new Color(1f, 0.98f, 0.94f, 1f);

            vitalLabelStyle = new GUIStyle("label");
            vitalLabelStyle.fontStyle = FontStyle.Bold;
            vitalLabelStyle.fontSize = 14;
            vitalLabelStyle.padding = new RectOffset(2, 2, 2, 2);
            vitalLabelStyle.alignment = TextAnchor.MiddleCenter;
            vitalLabelStyle.normal.textColor = new Color(1f, 0.98f, 0.94f, 1f);

            blockTextStyle = new GUIStyle();
            blockTextStyle.fontSize = 18;
            blockTextStyle.padding = new RectOffset(2, 2, 2, 2);
            blockTextStyle.alignment = TextAnchor.UpperLeft;
            blockTextStyle.wordWrap = true;
            blockTextStyle.normal.textColor = new Color(1f, 0.98f, 0.94f, 1f);
        }

        public static void Label(Rect rect, string text)
        {
            GUI.Label(rect, text, labelStyle);
        }

        public static void FillableBar(Rect rect, float percent, Vital vital, Color fillColor)
        {
            DrawTextureWithBorder(rect, Res.textures["fillable_border"]);
            rect = rect.Contract(2f);
            GUI.DrawTexture(rect.Width(rect.width * percent), Res.TextureUnicolor(fillColor));
            GUI.Label(rect, Mathf.Round(vital.currentValue).ToString() + " / " + vital.value.ToString(), vitalLabelStyle);
        }

        public static void FillableBarWithLabelValue(Rect rect, string name, Vital vital, Color fillColor)
        {
            float percent = GameUtils.Normalize(0, vital.value, vital.currentValue);
            Rect[] hGrid = rect.HorizontalGrid(new[] {70, rect.width - 140, 70}, 5);
            Label(hGrid[1], name);
            FillableBar(hGrid[2], percent, vital, fillColor);
            Label(hGrid[3], Mathf.Round(percent * 100) + "%");
        }

        public static void SimpleStat(Rect rect, string text, float value, float baseValue = -1f)
        {
            Rect[] hGrid = rect.HorizontalGrid(new[] {rect.width - 70, 70}, 5);

            Label(hGrid[1], text);
            Label(hGrid[2], "<b>" + value + "</b>");
        }

        public static Dictionary<Rect, Rect> SplitTextureBorderUV(Rect r, float textureWidth)
        {
            int k = (int) ((r.width + textureWidth) + r.height * 666);
            if (subRectCache.ContainsKey(k))
            {
                return subRectCache[k];
            }

            Dictionary<Rect, Rect> result = new Dictionary<Rect, Rect>();
            float q = textureWidth / 4f;
            result.Add(
                new Rect(0, 0, q, q),
                new Rect(0, 0, .25f, .25f)
            );
            result.Add(
                new Rect(r.width - q, 0, q, q),
                new Rect(.75f, 0f, .25f, .25f)
            );
            result.Add(
                new Rect(0, r.height - q, q, q),
                new Rect(0f, 0.75f, 0.25f, 0.25f)
            );
            result.Add(
                new Rect(r.width - q, r.height - q, q, q),
                new Rect(.75f, .75f, .25f, .25f)
            );
            result.Add(
                new Rect(q, q, r.width - q * 2f, r.height - q * 2f),
                new Rect(.25f, .25f, .5f, .5f)
            );
            result.Add(
                new Rect(q, 0f, r.width - q * 2f, q),
                new Rect(.25f, 0f, .5f, .25f)
            );
            result.Add(
                new Rect(q, r.height - q, r.width - q * 2f, q),
                new Rect(.25f, .75f, .5f, .25f)
            );
            result.Add(
                new Rect(0, q, q, r.height - q * 2f),
                new Rect(0, .25f, .25f, .5f)
            );
            result.Add(
                new Rect(r.width - q, q, q, r.height - q * 2f),
                new Rect(0.75f, .25f, .25f, .5f)
            );
            subRectCache.Add(k, result);
            return subRectCache[k];
        }


        public static void DrawTextureWithBorder(Rect rect, Texture2D texture)
        {
            Rect r = rect.RoundToInt();
            GUI.BeginGroup(r);

            foreach (KeyValuePair<Rect, Rect> kv in SplitTextureBorderUV(r, texture.width))
            {
                GUI.DrawTextureWithTexCoords(kv.Key, texture, kv.Value.InvertY());
            }

            GUI.EndGroup();
        }
    }
}