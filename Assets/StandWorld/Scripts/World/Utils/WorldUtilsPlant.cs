using System.Collections.Generic;
using System.Linq;
using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.Game;
using UnityEngine;

namespace StandWorld.World
{
    public class WorldUtilsPlant : Singleton<WorldUtilsPlant>
    {
        public readonly List<Tilable> cutOrdered = new List<Tilable>();

        public Tilable FieldNextToCut(Vector2Int characterPosition)
        {
            List<Tilable> toCut = new List<Tilable>();

            foreach (GrowArea growArea in GrowArea.growAreas)
            {
                foreach (Vector2Int position in growArea.positions)
                {
                    Tilable tilable = ToolBox.Instance.map.grids[Layer.Plant].GetTilableAt(position);
                    if (!ToolBox.Instance.map[position].reserved && tilable != null && tilable.tilableDef != growArea.def)
                    {
                        toCut.Add(tilable);
                    }
                }
            }

            return WorldUtils.ClosestTilableFromEnumerable(characterPosition, toCut);
        }

        public Tilable NextToCut(Vector2Int playerPosition)
        {
            List<Tilable> toCut = new List<Tilable>();
            foreach (Tilable tilable in cutOrdered)
            {
                if (!ToolBox.Instance.map[tilable.position].reserved)
                {
                    toCut.Add(tilable);
                }
            }

            return WorldUtils.ClosestTilableFromEnumerable(playerPosition, toCut);
        }

        public Tilable FieldNextTileToDirt(Vector2Int characterPosition)
        {
            List<Tilable> toDirt = new List<Tilable>();
            foreach (GrowArea growArea in GrowArea.growAreas)
            {
                foreach (Vector2Int position in growArea.positions)
                {
                    GardenField gardenField = (GardenField) ToolBox.Instance.map.grids[Layer.Helpers].GetTilableAt(position);
                    if (gardenField != null) // TODO: Temp
                    {
                        if (!ToolBox.Instance.map[position].reserved && gardenField.dirt == false)
                        {
                            toDirt.Add(gardenField);
                        }
                    }
                }
            }

            return WorldUtils.ClosestTilableFromEnumerable(characterPosition, toDirt);
        }

        public Tilable FieldNextTileToSow(Vector2Int playerPosition)
        {
            List<Tilable> toSow = new List<Tilable>();
            foreach (GrowArea growArea in GrowArea.growAreas)
            {
                foreach (Vector2Int position in growArea.positions)
                {
                    Tilable tilable = ToolBox.Instance.map.grids[Layer.Plant].GetTilableAt(position);
                    GardenField gardenField = (GardenField) ToolBox.Instance.map.grids[Layer.Helpers].GetTilableAt(position);
                    if (gardenField != null) // TODO: Temp
                    {
                        if (!ToolBox.Instance.map[position].reserved && tilable == null && gardenField.dirt)
                        {
                            toSow.Add(gardenField);
                        }
                    }
                }
            }

            return WorldUtils.ClosestTilableFromEnumerable(playerPosition, toSow);
        }

        public bool FieldHasWork()
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

        public bool FieldHasPlantsToCut()
        {
            foreach (GrowArea growArea in GrowArea.growAreas)
            {
                foreach (Vector2Int position in growArea.positions)
                {
                    Tilable tilable = ToolBox.Instance.map.grids[Layer.Plant].GetTilableAt(position);
                    if (!ToolBox.Instance.map[position].reserved && tilable != null && tilable.tilableDef != growArea.def &&
                        tilable.tilableDef.cuttable)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool FieldHasPlantsToSow()
        {
            foreach (GrowArea growArea in GrowArea.growAreas)
            {
                foreach (Vector2Int position in growArea.positions)
                {
                    Tilable tilable = ToolBox.Instance.map.grids[Layer.Plant].GetTilableAt(position);
                    GardenField gardenField = (GardenField) ToolBox.Instance.map.grids[Layer.Helpers].GetTilableAt(position);
                    if (gardenField != null) // TODO: Temp
                    {
                        if (!ToolBox.Instance.map[position].reserved && tilable == null && gardenField.dirt)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public bool FieldHasDirtToWork()
        {
            foreach (GrowArea growArea in GrowArea.growAreas)
            {
                foreach (Vector2Int position in growArea.positions)
                {
                    GardenField gardenField = (GardenField) ToolBox.Instance.map.grids[Layer.Helpers].GetTilableAt(position);
                    if (gardenField != null) // TODO: Temp
                    {
                        if (!ToolBox.Instance.map[position].reserved && gardenField.dirt == false)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        public bool HasPlantToCut()
        {
            return cutOrdered.Count > 0;
        }

        public BucketResult HasVegetalNutrimentsInBucket(Vector2Int position)
        {
            foreach (LayerGrid grid in ToolBox.Instance.map.grids.Values)
            {
                LayerBucketGrid bucket = grid.GetBucketAt(position);

                if (bucket != null && bucket.properties.vegetalNutriments > 0f)
                {
                    Tilable rt = WorldUtils.ClosestTilableFromEnumerable(
                        position,
                        bucket.tilables.Where(
                            t =>
                                t != null &&
                                ToolBox.Instance.map[t.position].reserved == false
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