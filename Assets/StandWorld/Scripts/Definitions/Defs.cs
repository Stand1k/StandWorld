using System.Collections.Generic;
using UnityEngine;

namespace StandWorld.Definitions
{
    public static class Defs
    {
        public static Dictionary<string, TilableDef> grounds;
        
        public static SortedDictionary<float, TilableDef> groundsByHeight;

        public static Dictionary<string, ColorPaletteDef> colorPallets;
        
        public static Dictionary<string, TilableDef> plants;
        
        public static Dictionary<string, TilableDef> mountains;
        
        public static Dictionary<string, TilableDef> stackables;

        public static void AddGround(TilableDef def)
        {
             grounds.Add(def.uID, def);
        }
        
        public static void AddPlant(TilableDef def)
        {
            plants.Add(def.uID, def);
        }
        
        public static void AddMountain(TilableDef def)
        {
            mountains.Add(def.uID, def);
        }

        public static void AddColorPalette(ColorPaletteDef def)
        {
            colorPallets.Add(def.uID, def);
        }
        
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

        public static void LoadPlantsFromCode()
        {
            plants = new Dictionary<string, TilableDef>();
            
            AddPlant(
                new TilableDef
                {
                    uID = "grass",
                    layer = Layer.Plant,
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
                    graphics = new GraphicDef
                    {
                        textureName = "tree",
                        size = new Vector2(2,3),
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

        public static void LoadGroundsFromCode()
        {
            grounds = new Dictionary<string, TilableDef>();
            groundsByHeight = new SortedDictionary<float, TilableDef>();

            AddGround(
                new TilableDef
                {
                    uID =  "dirt",
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
                    uID =  "water",
                    layer = Layer.Ground,
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
                    uID =  "rock",
                    layer = Layer.Ground,
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

        public static void LoadColorPalettesFromCode()
        {
            colorPallets = new Dictionary<string, ColorPaletteDef>();
            
            AddColorPalette(new ColorPaletteDef{
                uID = "cols_leafsGreen",
                colors = new List<Color>{
                    new Color(0.63f, 0.76f, 0.29f),
                    new Color(0.53f, 0.64f, 0.19f),
                    new Color(0.74f, 0.78f, 0.4f)
                }
            });
            AddColorPalette(new ColorPaletteDef{
                uID = "cols_leafsOrange",
                colors = new List<Color>{
                    new Color(0.73f, 0.4f, 0f),
                    new Color(0.67f, 0.31f, 0f),
                    new Color(0.73f, 0.35f, 0.19f)
                }
            });
            AddColorPalette(new ColorPaletteDef{
                uID = "cols_wood",
                colors = new List<Color>{
                    new Color(0.63f, 0.37f, 0.22f),
                    new Color(0.48f, 0.27f, 0.18f),
                    new Color(0.46f, 0.22f, 0.09f)
                }
            });
        }
    }
    
}