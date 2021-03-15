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
                    layer = Layer.Mountain,
                    graphics = new GraphicDef
                    {
                        textureName = "mountain",
                        color = new Color(0.91f, 0.91f, 0.91f)
                    }
                }
            );
        }
    }
}