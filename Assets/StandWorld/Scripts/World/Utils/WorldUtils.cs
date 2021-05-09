using System.Collections.Generic;
using System.Linq;
using StandWorld.Definitions;
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

    public static partial class WorldUtils
    {
        public static Tilable ClosestTilableFromEnumerable(Vector2Int position, IEnumerable<Tilable> tilables)
        {
            Tilable result = null;
            float minDistance = float.MaxValue;
            foreach (Tilable tilable in tilables)
            {
                float currentMinDistance = GameUtils.Distance(position, tilable.position);
                if (currentMinDistance < minDistance)
                {
                    minDistance = currentMinDistance;
                    result = tilable;
                }
            }

            return result;
        }
    }
}