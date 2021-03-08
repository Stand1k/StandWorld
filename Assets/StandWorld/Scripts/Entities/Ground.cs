using UnityEngine;
using StandWorld.Definitions;
using StandWorld.Visuals;

namespace StandWorld.Entities
{
    public class Ground : Tilable
    {
        public Ground(Vector2Int position, TilableDef def)
        {
            this.position = position;
            this.def = def;
            this.mainGraphic = GraphicInstance.GetNew(def.graphics);
        }

        public static TilableDef GroundByHeight(float height)
        {
            foreach (TilableDef tilableDef in Defs.groundsByHeight.Values)
            {
                if (height <= tilableDef.groundDef.maxHeight)
                {
                    return tilableDef;
                }
            }

            return Defs.grounds["water"];
        }
    }
}