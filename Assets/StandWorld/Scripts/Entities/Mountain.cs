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
        
        public Mountain(Vector2Int position, TilableDef tilableDef)
        {
            this.position = position;
            this.tilableDef = tilableDef;
            
            mainGraphic = GraphicInstance.GetNew(
                this.tilableDef.graphics,
                default(Color),
                Res.textures[this.tilableDef.graphics.textureName + "_0"],
                1
            );
            
            _connectedTilable = new ConnectedTilable(this);
            addGraphics = new Dictionary<string, GraphicInstance>();
        }

        public override void UpdateGraphics()
        {
            _connectedTilable.UpdateGraphics();
        }
    }
}
