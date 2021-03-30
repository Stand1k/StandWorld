using System.Collections.Generic;
using UnityEngine;

namespace StandWorld.Definitions
{
    public static partial class Defs
    {
        public static void AddColorPalette(ColorPaletteDef def)
        {
            colorPallets.Add(def.uID, def);
        } 
        public static void AddColorPalette(NamedColorPaletteDef def)
        {
            namedColorPallets.Add(def.uID, def);
        }
        
        public static void LoadColorPalettesFromCode()
        {
            colorPallets = new Dictionary<string, ColorPaletteDef>();
            namedColorPallets = new Dictionary<string, NamedColorPaletteDef>();
            
            AddColorPalette(new NamedColorPaletteDef{
                uID = "cols_vitals",
                colors = new Dictionary<string, Color>{
                    {"Health", new Color(0.37f, 0.07f, 0.07f)},
                    {"Energy", new Color(0.18f, 0.3f, 0.09f)},
                    {"Mana", new Color(0.05f, 0.1f, 0.34f)},
                    {"Joy", new Color(0.32f, 0.06f, 0.32f)},
                }
            });
            
            AddColorPalette(new ColorPaletteDef{
                uID = "cols_leafsGreen",
                colors = new List<Color>{
                    new Color(0.63f, 0.76f, 0.29f),
                    new Color(0.53f, 0.64f, 0.19f),
                    new Color(0.74f, 0.78f, 0.4f)
                }
            });
            AddColorPalette(new ColorPaletteDef{
                uID = "cols_leafsOrange",
                colors = new List<Color>{
                    new Color(0.73f, 0.4f, 0f),
                    new Color(0.67f, 0.31f, 0f),
                    new Color(0.73f, 0.35f, 0.19f)
                }
            });
            AddColorPalette(new ColorPaletteDef{
                uID = "cols_wood",
                colors = new List<Color>{
                    new Color(0.63f, 0.37f, 0.22f),
                    new Color(0.48f, 0.27f, 0.18f),
                    new Color(0.46f, 0.22f, 0.09f)
                }
            });
        }
    }
}
