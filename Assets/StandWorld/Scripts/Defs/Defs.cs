using System.Collections.Generic;

namespace StandWorld.Definitions
{
    public static class Defs
    {
        public static Dictionary<string, TilableDef> grounds;
        
        public static SortedDictionary<float, TilableDef> groundsByHeight;
        
        public static Dictionary<string, TilableDef> plants;

        public static void AddGround(TilableDef def)
        {
             grounds.Add(def.uID, def);
        }
        
        public static void AddPlant(TilableDef def)
        {
            plants.Add(def.uID, def);
        }

        public static void LoadPlantsFromCode()
        {
            plants = new Dictionary<string, TilableDef>();
            
            AddPlant(
                new TilableDef
                {
                    uID = "grass",
                    layer = Layer.Grass,
                    graphics = new GraphicDef
                    {
                        textureName = "grass"
                    }
                }
                );
        }

        public static void LoadGroundsFromCode()
        {
            grounds = new Dictionary<string, TilableDef>();
            groundsByHeight = new SortedDictionary<float, TilableDef>();

            AddGround(
                new TilableDef
                {
                uID =  "dirt",
                layer = Layer.Ground,
                graphics = new GraphicDef
                {
                    textureName = "dirt",
                    materialName = "grounds",
                    isInstanced = false
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
                    uID =  "water",
                    layer = Layer.Ground,
                    graphics = new GraphicDef
                    {
                        textureName = "water",
                        materialName = "grounds",
                        isInstanced = false
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
                    uID =  "rock",
                    layer = Layer.Ground,
                    graphics = new GraphicDef
                    {
                        textureName = "rock",
                        materialName = "grounds",
                        isInstanced = false
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