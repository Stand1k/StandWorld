using System.Collections.Generic;
using StandWorld.Definitions;
using StandWorld.Game;
using StandWorld.Helpers;
using StandWorld.Visuals;
using UnityEngine;

namespace StandWorld.Entities
{
    public class Building : Tilable
    {
        private ConnectedTilable _connectedUtility;
        public Recipe recipe { get; protected set; }

        public bool isBlueprint => tilableDef.buildingDef.work == work;

        public int work;

        public Building(Vector2Int position, TilableDef tilableDef)
        {
            this.position = position;
            this.tilableDef = tilableDef;
            recipe = new Recipe(this.tilableDef.recipeDef, this, position);
            work = 0;

            if (this.tilableDef.type == TilableType.BuildingConnected)
            {
                mainGraphic = GraphicInstance.GetNew(
                    this.tilableDef.graphics,
                    new Color(0.44f, 0.31f, 0.18f, 0.5f), // змінити альфу після збудування
                    Res.textures[this.tilableDef.graphics.textureName + "_0"],
                    1
                );
                _connectedUtility = new ConnectedTilable(this);
            }

            addGraphics = new Dictionary<string, GraphicInstance>();

            Tilable tilable = ToolBox.map.grids[Layer.Plant].GetTilableAt(this.position);
            if (tilable != null && tilable.tilableDef.cuttable)
            {
                tilable.AddOrder(Defs.orders["cut_plants"]);
            }
        }

        public void Construct()
        {
            if (mainGraphic.texture == null)
            {
                Debug.LogError(mainGraphic.ToString());
            }
            mainGraphic = GraphicInstance.GetNew(
                tilableDef.graphics,
                new Color(0.44f, 0.31f, 0.18f, 1f), // після збування альфа == 1
                mainGraphic.texture,
                1
            );
            
            UpdateGraphics();
            
            if (bucket != null)
            {
                bucket.rebuildMatrices = true;
            }
        }

        public override void UpdateGraphics()
        {
            if (tilableDef.type == TilableType.BuildingConnected)
            {
                _connectedUtility.UpdateGraphics();
            }
        }
    }
}