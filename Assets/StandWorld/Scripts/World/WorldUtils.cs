using System.Collections.Generic;
using System.Linq;
using StandWorld.Entities;
using StandWorld.Game;
using StandWorld.Helpers;
using UnityEngine;

namespace StandWorld.World
{
    public struct BucketResult
    {
        public bool result;
        public LayerBucketGrid bucket;
        public Tilable tilable;
    }

    public static class WorldUtils
    {
        public static Tilable ClosestTilableFromEnumarable(Vector2Int position, IEnumerable<Tilable> tilables)
        {
            Tilable result = null;
            float minDistance = float.MaxValue;
            foreach (Tilable tilable in tilables)
            {
                float currentMinDistance = Utils.Distance(position, tilable.position);
                if (currentMinDistance < minDistance)
                {
                    minDistance = currentMinDistance;
                    result = tilable;
                }
            }

            return result;
        }

        public static BucketResult HasVegetalNutrimentsInBucket(Vector2Int position)
        {
            foreach (LayerGrid grid in ToolBox.map.grids.Values)
            {
                LayerBucketGrid bucket = grid.GetBucketAt(position);

                if (bucket != null && bucket.properties.vegetalNutriments > 0f)
                {
                    Tilable rt = WorldUtils.ClosestTilableFromEnumarable(
                        position,
                        bucket.tilables.Where(
                            t =>
                                t != null && 
                                ToolBox.map[t.position].reserved == false 
                                && t.def.nutriments > 0)
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