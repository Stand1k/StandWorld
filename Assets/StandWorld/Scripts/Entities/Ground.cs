using UnityEngine;
using StandWorld.Definitions;

namespace StandWorld.Entities
{
    public class Ground : Tilable
    {
        public Ground(Vector2Int position, TilableDef def)
        {
            this.position = position;
            this.def = def;
        }
    }
}