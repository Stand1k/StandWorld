using System.Collections.Generic;
using StandWorld.Definitions;
using StandWorld.Helpers;
using StandWorld.Visuals;
using UnityEngine;

namespace StandWorld.Entities
{
    public class Stackable : Tilable
    {
        public InventoryTilable inventory { get; protected set; }

        public Stackable(Vector2Int position, TilableDef def, int count)
        {
            this.position = position;
            this.def = def;
            inventory = new InventoryTilable(this, count);
            mainGraphic = GraphicInstance.GetNew(this.def.graphics);
            
            ToolBox.stackableLabelController.AddLabel(this);
        }
        
    }
}