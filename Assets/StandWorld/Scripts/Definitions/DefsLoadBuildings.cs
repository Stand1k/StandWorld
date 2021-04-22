using System.Collections.Generic;

namespace StandWorld.Definitions
{
    public static partial class Defs
    {
        public static void AddBuilding(TilableDef tilableDef)
        {
            buildings.Add(tilableDef.uId, tilableDef);
        }

        public static void LoadBuildingsFromCode()
        {
            buildings = new Dictionary<string, TilableDef>();
            RecipeDef recipe = new RecipeDef();
            recipe.reqs.Add(stackables["logs"], 2);

            AddBuilding(new TilableDef
            {
                uId = "wood_wall",
                layer = Layer.Building,
                type = TilableType.BuildingConnected,
                blockPath = false,
                blockStackable = true,
                blockPlant = true,
                blockBuilding = true,
                graphics = new GraphicDef
                {
                    textureName = "wall"
                },
                recipeDef = recipe
            });
        }
    }
}