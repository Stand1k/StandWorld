using StandWorld.Definitions;
using StandWorld.Game;
using StandWorld.Helpers;
using StandWorld.Visuals;
using StandWorld.World;
using UnityEngine;

namespace StandWorld.Entities
{
    public class Stackable : Tilable
    {
        public Inventory inventory { get; protected set; }
        public StockArea stockArea { get; protected set; }

        public Stackable(Vector2Int position, TilableDef tilableDef, int count)
        {
            this.position = position;
            this.tilableDef = tilableDef;
            inventory = new Inventory(this, count);
            mainGraphic = GraphicInstance.GetNew(this.tilableDef.graphics);
            stockArea = null;
            SetNeigbours();

            ToolBox.Instance.stackableLabelController.AddLabel(this);

            if (count > 0)
            {
                WorldUtilsHaul.Instance.AddStackable(tilableDef, this);
            }

            inventory.OnClear += () => WorldUtilsHaul.Instance.ClearStackable(inventory.def, this);
            inventory.OnAdd += () => WorldUtilsHaul.Instance.AddStackable(inventory.def, this);
            inventory.OnChangeCount += change => WorldUtilsHaul.Instance.UpdateStackableCount(inventory.def, change);
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