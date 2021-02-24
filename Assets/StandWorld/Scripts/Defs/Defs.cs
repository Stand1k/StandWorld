using UnityEngine;
using System.Collections.Generic;

namespace StandWorld.Definitions
{
    public static class Defs
    {
        public static Dictionary<string, TilableDef> grounds;

        public static void AddGround(TilableDef def)
        {
             Defs.grounds.Add(def.uID,def);
        }

        public static void LoadGroundsFromCode()
        {
            Defs.grounds = new Dictionary<string, TilableDef>();
            Defs.AddGround(
                new GroundDef()
            {
                uID =  "dirt",
                layer = Layer.Ground,
                graphics = new GraphicDef
                {
                    textureName = "dirt"
                }
            }
                );
            
            Defs.AddGround(
                new GroundDef()
            {
                uID =  "water",
                layer = Layer.Ground,
                graphics = new GraphicDef
                {
                    textureName = "water"
                }
            }
                );
        }
    }
}