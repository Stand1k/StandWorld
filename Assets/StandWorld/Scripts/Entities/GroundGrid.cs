using StandWorld.Definitions;
using StandWorld.Visuals;
using StandWorld.World;
using UnityEngine;

namespace StandWorld.Entities
{
    public class GroundGrid : LayerGrid
    {
        public GroundGrid(Vector2Int size) : base(size, Layer.Ground)
        {
            renderer = typeof(BucketGroundRenderer);
            GenerateBuckets();
        }
    }
}