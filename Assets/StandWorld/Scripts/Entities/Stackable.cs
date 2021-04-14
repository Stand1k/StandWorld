using StandWorld.Definitions;
using StandWorld.Game;
using StandWorld.Helpers;
using StandWorld.Visuals;
using UnityEngine;

namespace StandWorld.Entities
{
    public class Stackable : Tilable
    {
        public InventoryTilable inventory { get; protected set; }
        public StockArea stockArea { get; protected set; }

        public Stackable(Vector2Int position, TilableDef tilableDef, int count)
        {
            this.position = position;
            this.tilableDef = tilableDef;
            inventory = new InventoryTilable(this, count);
            mainGraphic = GraphicInstance.GetNew(this.tilableDef.graphics);
            stockArea = null;

            ToolBox.stackableLabelController.AddLabel(this);
        }

        public Stackable(Vector2Int position, StockArea stockArea)
        {
            this.position = position;
            tilableDef = Defs.empty;
            this.stockArea = stockArea;
            inventory = null;
            mainGraphic = GraphicInstance.GetNew(
                tilableDef.graphics,
                stockArea.color,
                Res.TextureUnicolor(stockArea.color),
                100
            );
        }
    }
}