using System.Collections.Generic;
using UnityEngine;

namespace StandWorld.Definitions
{
    public static partial class Defs
    {
        public static Dictionary<string, TilableDef> grounds;

        public static SortedDictionary<float, TilableDef> groundsByHeight;

        public static Dictionary<string, ColorPaletteDef> colorPallets;
        
        public static Dictionary<string, NamedColorPaletteDef> namedColorPallets;

        public static Dictionary<string, TilableDef> plants;

        public static Dictionary<string, TilableDef> mountains;

        public static Dictionary<string, TilableDef> stackables;
        
        public static Dictionary<string, AnimalDef> animals;
        
        public static Dictionary<string, TaskDef> tasks;
    }
}