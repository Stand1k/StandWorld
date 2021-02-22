using UnityEngine;
using  StandWorld.Definitions;

namespace StandWorld.Entities
{
    public class Tilable 
    {
        public Vector2Int position { get; protected set; }
        
        public TilableDef def { get; protected set; }
    }
}
