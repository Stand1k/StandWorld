using StandWorld.Definitions;
using StandWorld.Visuals;
using UnityEngine;

namespace StandWorld.Entities
{
    public class Plant : Tilable 
    {
        public Plant(Vector2Int position, TilableDef def)
        {
            this.position = position;
            this.def = def;
            this.graphics = GraphicInstance.GetNew(def.graphics, Color.green);
        }
    }
}
