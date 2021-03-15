using System;
using System.Collections.Generic;
using StandWorld.Definitions;
using StandWorld.Helpers;
using StandWorld.Visuals;
using UnityEngine;

namespace StandWorld.Entities
{
    public class Mountain : Tilable
    {
        private ConnectedTilable _connectedTilable;
        
        public Mountain(Vector2Int position, TilableDef def)
        {
            this.position = position;
            this.def = def;
            _connectedTilable = new ConnectedTilable(this);
            mainGraphic = GraphicInstance.GetNew(
                this.def.graphics,
                default(Color),
                Res.textures[this.def.graphics.textureName + "_0"],
                1
            );
            addGraphics = new Dictionary<string, GraphicInstance>();
        }

        public override void UpdateGraphics()
        {
            _connectedTilable.UpdateGraphics();
        }
    }
}
