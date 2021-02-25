using System.Collections.Generic;

namespace StandWorld.Definitions
{
    public static class Defs
    {
        public static Dictionary<string, TilableDef> grounds;
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
                new PlantDef
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
            AddGround(
                new GroundDef
                {
                uID =  "dirt",
                layer = Layer.Ground,
                graphics = new GraphicDef
                {
                    textureName = "dirt",
                    materialName = "grounds",
                    isInstanced = false
                }
            }
                );
            
            AddGround(
                new GroundDef
                {
                uID =  "water",
                layer = Layer.Ground,
                graphics = new GraphicDef
                {
                    textureName = "water",
                    materialName = "grounds",
                    isInstanced = false
                }
            }
                );
        }
    }
}