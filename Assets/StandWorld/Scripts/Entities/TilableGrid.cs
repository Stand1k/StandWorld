using StandWorld.Definitions;
using StandWorld.World;
using UnityEngine;

namespace StandWorld.Entities
{
    public class TilableGrid : LayerGrid
    {
        public TilableGrid(Vector2Int size) : base(size, Layer.Plant)
        {
            renderer = null;
            GenerateBuckets();
        }
    }
}
