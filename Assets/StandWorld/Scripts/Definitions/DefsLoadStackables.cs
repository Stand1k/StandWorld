using System.Collections.Generic;
using UnityEngine;

namespace StandWorld.Definitions
{
    public static partial class Defs
    {
        public static void AddStackable(TilableDef def)
        {
            stackables.Add(def.uId, def);
        }

        public static void LoadStackablesFromCode()
        {
            stackables = new Dictionary<string, TilableDef>();

            AddStackable(
                new TilableDef
                {
                    uId = "logs",
                    layer = Layer.Stackable,
                    blockStackable = true,
                    graphics = new GraphicDef
                    {
                        textureName = "logs_stack",
                    },
                    maxStack = 10
                });

            AddStackable(
                new TilableDef
                {
                    uId = "carrot_logs",
                    layer = Layer.Stackable,
                    blockStackable = true,
                    graphics = new GraphicDef
                    {
                        textureName = "carrot_stack",
                    },
                    maxStack = 8
                });
        }
    }
}