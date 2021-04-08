using System.Collections.Generic;
using UnityEngine;

namespace StandWorld.Definitions
{
    public static partial class Defs
    {
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
                    uID = "carrot",
                    layer = Layer.Plant,
                    pathCost = .95f,
                    blockPlant = true,
                    nutriments = 2f,
                    blockStackable = true,
                    type = TilableType.Plant,
                    graphics = new GraphicDef
                    {
                        textureName = "carrot"
                    },
                    plantDef = new PlantDef
                    {
                        probability = 0.1f,
                        minFertility = 0.1f
                    }
                }
            );
            
            AddPlant(
                new TilableDef
                {
                    uID = "grass",
                    layer = Layer.Plant,
                    pathCost = .95f,
                    blockPlant = true,
                    nutriments = 1f,
                    blockStackable = true,
                    type = TilableType.Grass,
                    graphics = new GraphicDef
                    {
                        textureName = "grass"
                    },
                    plantDef = new PlantDef
                    {
                        probability = 0.2f,
                        minFertility = 0.3f
                    }
                }
            );

            AddPlant(
                new TilableDef
                {
                    uID = "tree",
                    layer = Layer.Plant,
                    type = TilableType.Tree,
                    blockPath = true,
                    blockStackable = true,
                    blockPlant = true,
                    graphics = new GraphicDef
                    {
                        textureName = "tree",
                        size = new Vector2(2, 3),
                        pivot = new Vector2(0.5f, 0f)
                    },
                    plantDef = new PlantDef
                    {
                        probability = 0.1f,
                        minFertility = 0.2f
                    }
                }
            );
        }

    }
}