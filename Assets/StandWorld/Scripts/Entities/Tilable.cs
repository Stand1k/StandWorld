using UnityEngine;
using  StandWorld.Definitions;
using StandWorld.Visuals;

namespace StandWorld.Entities
{
    public class Tilable 
    {
        public Vector2Int position { get; protected set; }
        
        public TilableDef def { get; protected set; }
        
        public  GraphicInstance graphics { get; protected set; }
    }
}
