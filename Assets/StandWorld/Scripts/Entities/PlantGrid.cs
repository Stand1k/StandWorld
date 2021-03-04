using StandWorld.Definitions;
using StandWorld.World;
using UnityEngine;

namespace StandWorld.Entities
{
    public class PlantGrid : LayerGrid
    {
        public PlantGrid(Vector2Int size) : base(size, Layer.Plant)
        {
            renderer = null;
            GenerateBuckets();
        }
    }
}
