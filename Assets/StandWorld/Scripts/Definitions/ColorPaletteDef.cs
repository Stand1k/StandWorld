using System.Collections.Generic;
using UnityEngine;

namespace StandWorld.Definitions
{
    [System.Serializable]
    public class ColorPaletteDef : Def
    {
        public List<Color> colors = new List<Color>();

        public Color32 GetRandom()
        {
            return colors[Random.Range(0, colors.Count)];
        }

        public int GetRandomID()
        {
            return Random.Range(0, colors.Count);
        }
    }
}