﻿using System.Collections.Generic;
using StandWorld.Entities;
using UnityEngine;

namespace StandWorld.Definitions
{
    public static partial class Defs
    {
        public static TilableDef empty;

        public static void LoadDefaultDefs()
        {
            empty = new TilableDef
            {
                uId = "empty",
                layer = Layer.Helpers,
                graphics = new GraphicDef{}
            };
        }

        public static Dictionary<string, MenuOrderDef> orders;
        
        public static Dictionary<string, MenuOrderDef> buildingOrders;
        
        public static Dictionary<string, MenuOrderDef> zonesOrders;
        
        public static Dictionary<string, TilableDef> buildings;

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