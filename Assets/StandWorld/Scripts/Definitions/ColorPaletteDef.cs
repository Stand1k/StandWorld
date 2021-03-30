using System.Collections.Generic;
using UnityEngine;

namespace StandWorld.Definitions
{
    [System.Serializable]
    public class ColorPaletteDef : Def
    {
        public List<Color> colors = new List<Color>(15);

        public Color32 GetRandom()
        {
            return colors[Random.Range(0, colors.Count)];
        }
    }
    
    [System.Serializable]
    public class NamedColorPaletteDef : Def 
    {
        public Dictionary<string, Color> colors = new Dictionary<string, Color>();
    }
}