using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace StandWorld.Definitions
{
    public static class Defs
    {
        public static Dictionary<string, TilableDef> grounds;
        
        public static SortedDictionary<float, TilableDef> groundsByHeight;

        public static Dictionary<string, ColorPaletteDef> colorPallets;
        
        public static Dictionary<string, TilableDef> plants;

        public static void AddGround(TilableDef def)
        {
             grounds.Add(def.uID, def);
        }
        
        public static void AddPlant(TilableDef def)
        {
            plants.Add(def.uID, def);
        }

        public static void AddColorPalette(ColorPaletteDef def)
        {
            colorPallets.Add(def.uID, def);
        }

        public static void LoadPlantsFromCode()
        {
            plants = new Dictionary<string, TilableDef>();
            
            AddPlant(
                new TilableDef
                {
                    uID = "grass",
                    layer = Layer.Grass,
                    type = TilableType.Grass,
                    graphics = new GraphicDef
                    {
                        textureName = "grass"
                    },
                    plantDef = new PlantDef
                    {
                        probability = 0.3f,
                        minFertility = 0.1f
                    }
                }
                );
            
            AddPlant(
                new TilableDef
                {
                    uID = "tree",
                    layer = Layer.Tree,
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
            Defs.colorPallets = new Dictionary<string, ColorPaletteDef>();
            Defs.AddColorPalette(new ColorPaletteDef{
                uID = "cols_leafsGreen",
                colors = new List<Color>{
                    new Color(0.63f, 0.76f, 0.29f),
                    new Color(0.53f, 0.64f, 0.19f),
                    new Color(0.74f, 0.78f, 0.4f)
                }
            });
            Defs.AddColorPalette(new ColorPaletteDef{
                uID = "cols_leafsOrange",
                colors = new List<Color>{
                    new Color(0.73f, 0.4f, 0f),
                    new Color(0.67f, 0.31f, 1f, 1f),
                    new Color(0.73f, 0.35f, 0.19f)
                }
            });
            Defs.AddColorPalette(new ColorPaletteDef{
                uID = "cols_wood",
                colors = new List<Color>{
                    new Color(0.44f, 0.31f, 0.18f),
                    new Color(0.21f, 0.19f, 0.67f)
                }
            });
        }
    }
    
}