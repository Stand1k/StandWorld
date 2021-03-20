using System.Collections.Generic;
using UnityEngine;

namespace StandWorld.Definitions
{
    public static partial class Defs
    {
        public static void AddMountain(TilableDef def)
        {
            mountains.Add(def.uID, def);
        }
        
        public static void LoadMountainsFromCode()
        {
            mountains = new Dictionary<string, TilableDef>();
            
            AddMountain(
                new TilableDef
                {
                    uID = "mountain",
                    blockPath = true,
                    blockStackable = true,
                    supportRoof = true,
                    blockBuilding = true,
                    blockPlant = true,
                    layer = Layer.Mountain,
                    graphics = new GraphicDef
                    {
                        textureName = "mountain",
                        color = new Color(0.64f, 0.64f, 0.64f)
                    }
                }
            );
        }
    }
}