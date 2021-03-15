﻿using System.Collections.Generic;
using UnityEngine;

namespace StandWorld.Definitions
{
    public static partial class Defs 
    {
        public static void AddStackable(TilableDef def)
        {
            stackables.Add(def.uID, def);
        }
        
        public static void LoadStackablesFromCode()
        {
            stackables = new Dictionary<string, TilableDef>();
            
            AddStackable(
                new TilableDef
                {
                    uID = "logs",
                    layer = Layer.Stackable,
                    graphics = new GraphicDef
                    {
                        textureName = "logs_stack",
                        color = new Color(0.63f, 0.37f, 0.22f),
                    },
                    maxStack = 25
                }
            );
        }
    }
}