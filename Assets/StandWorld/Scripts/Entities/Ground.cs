﻿using UnityEngine;
using StandWorld.Definitions;
using StandWorld.Visuals;

namespace StandWorld.Entities
{
    public class Ground : Tilable
    {
        public Ground(Vector2Int position, TilableDef tilableDef)
        {
            this.position = position;
            this.tilableDef = tilableDef;
            mainGraphic = GraphicInstance.GetNew(tilableDef.graphics);
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

            return Defs.grounds["ground"];
        }
    }
}