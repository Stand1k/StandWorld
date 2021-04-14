using System.Collections.Generic;
using System.Linq;
using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.Game;
using UnityEngine;

namespace StandWorld.World
{
    public static partial class WorldUtils
    {
        public static List<Tilable> cutOrdered = new List<Tilable>();
        public static List<Recipe> recipes = new List<Recipe>();

        public static Tilable FieldNextToCut(Vector2Int characterPosition)
        {
            List<Tilable> toCut = new List<Tilable>();

            foreach (GrowArea growArea in GrowArea.growAreas)
            {
                foreach (Vector2Int position in growArea.positions)
                {
                    Tilable tilable = ToolBox.map.grids[Layer.Plant].GetTilableAt(position);
                    if (!ToolBox.map[position].reserved && tilable != null && tilable.tilableDef != growArea.def)
                    {
                        toCut.Add(tilable);
                    }
                }
            }

            return ClosestTilableFromEnumarable(characterPosition, toCut);
        }

        public static Tilable NextToCut(Vector2Int playerPosition)
        {
            List<Tilable> toCut = new List<Tilable>();
            foreach (Tilable tilable in WorldUtils.cutOrdered)
            {
                if (!ToolBox.map[tilable.position].reserved)
                {
                    toCut.Add(tilable);
                }
            }

            return ClosestTilableFromEnumarable(playerPosition, toCut);
        }

        public static Tilable FieldNextTileToDirt(Vector2Int characterPosition)
        {
            List<Tilable> toDirt = new List<Tilable>();
            foreach (GrowArea growArea in GrowArea.growAreas)
            {
                foreach (Vector2Int position in growArea.positions)
                {
                    Field field = (Field) ToolBox.map.grids[Layer.Helpers].GetTilableAt(position);
                    if (!ToolBox.map[position].reserved && field.dirt == false)
                    {
                        toDirt.Add(field);
                    }
                }
            }

            return ClosestTilableFromEnumarable(characterPosition, toDirt);
        }

        public static Tilable FieldNextTileToSow(Vector2Int playerPosition)
        {
            List<Tilable> toSow = new List<Tilable>();
            foreach (GrowArea growArea in GrowArea.growAreas)
            {
                foreach (Vector2Int position in growArea.positions)
                {
                    Tilable tilable = ToolBox.map.grids[Layer.Plant].GetTilableAt(position);
                    Field field = (Field) ToolBox.map.grids[Layer.Helpers].GetTilableAt(position);
                    if (!ToolBox.map[position].reserved && tilable == null && field.dirt)
                    {
                        toSow.Add(field);
                    }
                }
            }

            return ClosestTilableFromEnumarable(playerPosition, toSow);
        }

        public static bool FieldHasWork()
        {
            if (FieldHasPlantsToCut() ||
                FieldHasDirtToWork() ||
                FieldHasPlantsToSow()
            )
            {
                return true;
            }

            return false;
        }

        public static bool FieldHasPlantsToCut()
        {
            foreach (GrowArea growArea in GrowArea.growAreas)
            {
                foreach (Vector2Int position in growArea.positions)
                {
                    Tilable tilable = ToolBox.map.grids[Layer.Plant].GetTilableAt(position);
                    if (!ToolBox.map[position].reserved && tilable != null && tilable.tilableDef != growArea.def &&
                        tilable.tilableDef.cuttable)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool FieldHasPlantsToHarvest()
        {
            foreach (GrowArea growArea in GrowArea.growAreas)
            {
                foreach (Vector2Int position in growArea.positions)
                {
                    Plant plant = (Plant) ToolBox.map.grids[Layer.Plant].GetTilableAt(position);
                    if (!ToolBox.map[position].reserved && plant != null && plant.state == growArea.def.plantDef.states)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool FieldHasPlantsToSow()
        {
            foreach (GrowArea growArea in GrowArea.growAreas)
            {
                foreach (Vector2Int position in growArea.positions)
                {
                    Tilable tilable = ToolBox.map.grids[Layer.Plant].GetTilableAt(position);
                    Field field = (Field) ToolBox.map.grids[Layer.Helpers].GetTilableAt(position);
                    if (!ToolBox.map[position].reserved && tilable == null && field.dirt)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool FieldHasDirtToWork()
        {
            foreach (GrowArea growArea in GrowArea.growAreas)
            {
                foreach (Vector2Int position in growArea.positions)
                {
                    Field field = (Field) ToolBox.map.grids[Layer.Helpers].GetTilableAt(position);
                    if (!ToolBox.map[position].reserved && field.dirt == false)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool HasPlantToCut()
        {
            return cutOrdered.Count > 0;
        }

        public static BucketResult HasVegetalNutrimentsInBucket(Vector2Int position)
        {
            foreach (LayerGrid grid in ToolBox.map.grids.Values)
            {
                LayerBucketGrid bucket = grid.GetBucketAt(position);

                if (bucket != null && bucket.properties.vegetalNutriments > 0f)
                {
                    Tilable rt = ClosestTilableFromEnumarable(
                        position,
                        bucket.tilables.Where(
                            t =>
                                t != null &&
                                ToolBox.map[t.position].reserved == false
                                && t.tilableDef.nutriments > 0)
                    );

                    if (rt != null)
                    {
                        return new BucketResult
                        {
                            result = true,
                            bucket = bucket,
                            tilable = rt
                        };
                    }
                }
            }

            return new BucketResult
            {
                result = false,
                bucket = null,
                tilable = null
            };
        }
    }
}