using System.Collections.Generic;
using StandWorld.Definitions;
using StandWorld.Helpers;
using StandWorld.Visuals;
using UnityEngine;

namespace StandWorld.Entities
{
    public class Field : Tilable
    {
        public GrowArea growArea { get; protected set; }
        public bool dirt { get; protected set; }

        public Field(Vector2Int position, TilableDef def, GrowArea growArea)
        {
            addGraphics = new Dictionary<string, GraphicInstance>();
            this.growArea = growArea;
            dirt = false;
            this.position = position;
            this.def = def;
            mainGraphic = GraphicInstance.GetNew(
                def.graphics,
                growArea.color,
                Res.TextureUnicolor(growArea.color),
                1
            );

            addGraphics.Add("dirt",
                GraphicInstance.GetNew(
                    def.graphics,
                    Color.white,
                    Res.TextureUnicolor(Color.clear),
                    2
                )
            );
        }

        public void WorkDirt()
        {
            dirt = true;
            addGraphics["dirt"] = GraphicInstance.GetNew(
                def.graphics,
                Color.white,
                Res.textures["dirt_ready"],
                2
            );

            if (bucket != null)
            {
                bucket.rebuildMatrices = true;
            }
        }
    }
}