using System.Collections.Generic;
using UnityEngine;

namespace StandWorld.Definitions
{
    public static partial class Defs
    {
        public static void AddColorPalette(ColorPaletteDef def)
        {
            colorPallets.Add(def.uId, def);
        }

        public static void AddColorPalette(NamedColorPaletteDef def)
        {
            namedColorPallets.Add(def.uId, def);
        }

        public static void LoadColorPalettesFromCode()
        {
            colorPallets = new Dictionary<string, ColorPaletteDef>();
            namedColorPallets = new Dictionary<string, NamedColorPaletteDef>();

            AddColorPalette(new ColorPaletteDef
            {
                uId = "human_hair",
                colors = new List<Color>
                {
                    new Color(0.53f, 0.31f, 0.2f),
                    new Color(0.16f, 0.16f, 0.16f),
                    new Color(0.05f, 0.43f, 0.38f),
                    new Color(0.89f, 0.12f, 0.28f),
                }
            });

            AddColorPalette(new ColorPaletteDef
            {
                uId = "human_clothes",
                colors = new List<Color>
                {
                    Color.black,
                    Color.white,
                    new Color(0.35f, 0.78f, 1f),
                    new Color(0.51f, 0.52f, 0.53f),
                    new Color(0.66f, 0.03f, 0.03f),
                    new Color(0.87f, 0.81f, 0f),
                }
            });

            AddColorPalette(new ColorPaletteDef
            {
                uId = "human_body",
                colors = new List<Color>
                {
                    new Color(0.9f, 0.74f, 0.6f),
                    new Color(0.63f, 0.43f, 0.29f),
                    new Color(0.23f, 0.13f, 0.1f),
                }
            });

            AddColorPalette(new NamedColorPaletteDef
            {
                uId = "cols_vitals",
                colors = new Dictionary<string, Color>
                {
                    {"Health", new Color(0.34f, 0.07f, 0.07f)},
                    {"Energy", new Color(0.22f, 0.34f, 0.09f)},
                    {"Mana", new Color(0.05f, 0.1f, 0.34f)},
                    {"Joy", new Color(0.34f, 0.05f, 0.34f)},
                    {"Hunger", new Color(0.34f, 0.28f, 0f)},
                }
            });

            AddColorPalette(new ColorPaletteDef
            {
                uId = "cols_leafsGreen",
                colors = new List<Color>
                {
                    new Color(0.63f, 0.76f, 0.29f),
                    new Color(0.53f, 0.64f, 0.19f),
                    new Color(0.74f, 0.78f, 0.4f)
                }
            });
            
            AddColorPalette(new ColorPaletteDef
            {
                uId = "cols_leafsOrange",
                colors = new List<Color>
                {
                    new Color(0.73f, 0.4f, 0f),
                    new Color(0.67f, 0.31f, 0f),
                    new Color(0.73f, 0.35f, 0.19f)
                }
            });
            
            AddColorPalette(new ColorPaletteDef
            {
                uId = "cols_wood",
                colors = new List<Color>
                {
                    new Color(0.63f, 0.37f, 0.22f),
                    new Color(0.48f, 0.27f, 0.18f),
                    new Color(0.46f, 0.22f, 0.09f)
                }
            });
        }
    }
}