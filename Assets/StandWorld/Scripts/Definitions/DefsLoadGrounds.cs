using System.Collections.Generic;

namespace StandWorld.Definitions
{
    public static partial class Defs
    {
        public static void AddGround(TilableDef def)
        {
            grounds.Add(def.uId, def);
        }

        public static void LoadGroundsFromCode()
        {
            grounds = new Dictionary<string, TilableDef>();
            groundsByHeight = new SortedDictionary<float, TilableDef>();

            AddGround(
                new TilableDef
                {
                    uId =  "dirt",
                    layer = Layer.Ground,
                    fertility = 1f,
                    graphics = new GraphicDef
                    {
                        textureName = "dirt",
                        materialName = "grounds",
                        isInstanced = false,
                        drawPriority = 1
                    },
                    groundDef = new GroundDef
                    {
                        maxHeight = 0.6f
                    }
                }   
            );

            AddGround(
                new TilableDef
                {
                    uId =  "water",
                    layer = Layer.Ground,
                    blockPath = true,
                    blockBuilding = true,
                    graphics = new GraphicDef
                    {
                        textureName = "water",
                        materialName = "water",
                        isInstanced = false,
                        drawPriority = 0
                    },
                    groundDef = new GroundDef
                    {
                        maxHeight = 0.3f
                    }
                }
            );
            
            AddGround(
                new TilableDef
                {
                    uId =  "rock",
                    layer = Layer.Ground,
                    pathCost = 1.05f,
                    graphics = new GraphicDef
                    {
                        textureName = "rock",
                        materialName = "grounds",
                        isInstanced = false,
                        drawPriority = 2
                    },
                    groundDef = new GroundDef
                    {
                        maxHeight = 1f
                    }
                }
            );

            foreach (TilableDef tilableDef in grounds.Values)
            {
                groundsByHeight.Add(tilableDef.groundDef.maxHeight, tilableDef);
            }
        }
    }
}