﻿using System.Collections.Generic;
using UnityEngine;

namespace StandWorld.Definitions
{
    public static partial class Defs
    {
        public static void AddPlant(TilableDef def)
        {
            plants.Add(def.uId, def);
        }

        public static void LoadPlantsFromCode()
        {
            plants = new Dictionary<string, TilableDef>();

            AddPlant(
                new TilableDef
                {
                    uId = "carrot",
                    layer = Layer.Plant,
                    pathCost = .95f,
                    blockPlant = true,
                    nutriments = 2f,
                    blockStackable = true,
                    type = TilableType.Plant,
                    cuttable = true,
                    graphics = new GraphicDef
                    {
                        textureName = "carrot"
                    },
                    plantDef = new PlantDef
                    {
                        probability = 0.15f,
                        minFertility = 0.5f
                    }
                }
            );
            
            AddPlant(
                new TilableDef
                {
                    uId = "tree",
                    layer = Layer.Plant,
                    type = TilableType.Tree,
                    blockPath = true,
                    blockStackable = true,
                    blockPlant = true,
                    cuttable = true,
                    graphics = new GraphicDef
                    {
                        materialName = "plantsWind",
                        textureName = "tree",
                        size = new Vector2(2, 3),
                        pivot = new Vector2(0.5f, 0f)
                    },
                    plantDef = new PlantDef
                    {
                        probability = 0.36f,
                        minFertility = 0.2f,
                    }
                }
            );
            
            AddPlant(
                new TilableDef
                {
                    uId = "grass",
                    layer = Layer.Plant,
                    pathCost = .95f,
                    blockPlant = true,
                    nutriments = 0.3f,
                    blockStackable = true,
                    type = TilableType.Grass,
                    cuttable = true,
                    graphics = new GraphicDef
                    {
                        materialName = "plantsWind",
                        textureName = "grass"
                    },
                    plantDef = new PlantDef
                    {
                        probability = 0.47f,
                        minFertility = 0.3f
                    }
                }
            );
        }

    }
}