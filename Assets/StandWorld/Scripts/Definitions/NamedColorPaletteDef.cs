using System.Collections.Generic;
using UnityEngine;

namespace StandWorld.Definitions
{
    [System.Serializable]
    public class NamedColorPaletteDef : Def 
    {
        public Dictionary<string, Color> colors = new Dictionary<string, Color>();
    }
}