using System.Collections.Generic;
using StandWorld.Characters;
using StandWorld.Characters.AI;
using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.Game;
using StandWorld.Helpers;
using UnityEngine;

namespace StandWorld.World
{
    public static partial class WorldUtils
    {
        public static readonly List<Recipe> recipes = new List<Recipe>();

        public static bool HaulRecipeNeeded()
        {
            return recipes.Count > 0;
        }

        public static void SpawnBuilding(Vector2Int position) // TODO:
        {
            TileProperty tileProperty = ToolBox.map[position];

            if (tileProperty.blockBuilding)
            {
                return;
            }

            ToolBox.map.Spawn(new Vector2Int(position.x, position.y), new Building(
                new Vector2Int(position.x, position.y),
                Defs.buildings["wood_wall"]
            ));

            ToolBox.map.UpdateConnectedBuildings();
        }

        public static void DeleteBlueprint(Vector2Int position) // TODO:
        {
            if (ToolBox.map.GetTilableAt(position, Layer.Building) is Building building && building.isBlueprint)
            {
                recipes.Remove(building.recipe);
                building.Destroy();
                ToolBox.map.UpdateConnectedBuildings();
            }
        }

        public static TargetList RecipesToComplete(int radius, BaseCharacter character)
        {
            int capacity = character.inventory.max;
            int currentNeeds = 0;
            Recipe first = null;
            TilableDef need = null;
            List<Recipe> toHaul = new List<Recipe>();
            TargetList targets = null;

            foreach (Recipe _recipe in recipes)
            {
                if (!ToolBox.map[_recipe.position].reserved && !_recipe.finished && _recipe.canBeComplete)
                {
                    if (currentNeeds >= capacity)
                    {
                        currentNeeds = capacity;
                        break;
                    }

                    if (first == null)
                    {
                        first = _recipe;
                        need = first.FirstNeed();
                        currentNeeds += first.needs[need].free;
                        capacity = (capacity > stackablesCount[need])
                            ? stackablesCount[need]
                            : capacity;
                    }
                    else
                    {
                        if (GameUtils.Distance(first.position, _recipe.position) <= radius)
                        {
                            toHaul.Add(_recipe);
                            currentNeeds += _recipe.needs[need].free;
                        }
                    }
                }
            }

            if (first == null || need == null || toHaul.Count == 0)
            {
                return null;
            }

            int stackFound = 0;
            if (stackables.ContainsKey(need))
            {
                foreach (Stackable stack in ToolBox.map.grids[Layer.Stackable].GetTilables())
                {
                    if (stackFound >= currentNeeds)
                    {
                        break;
                    }

                    if (!ToolBox.map[stack.position].reserved && stack.inventory.count != 0 &&
                        stack.inventory.def == need)
                    {
                        stackFound += stack.inventory.count;
                        if (targets == null)
                        {
                            targets = new TargetList(stack);
                        }
                        else
                        {
                            targets.Enqueue(stack);
                        }
                    }
                }
            }
            else
            {
                return null;
            }

            if (targets != null)
            {
                foreach (Recipe _recipe in toHaul)
                {
                    targets.Enqueue(_recipe.building);
                    break;
                }
            }

            return targets;
        }
    }
}