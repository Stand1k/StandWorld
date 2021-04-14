using System.Collections.Generic;
using StandWorld.Definitions;
using StandWorld.Game;
using StandWorld.Helpers;
using StandWorld.Visuals;
using StandWorld.World;
using UnityEngine;

namespace StandWorld.Entities
{
    public class Recipe
    {
        public RecipeDef recipeDef;
        public Dictionary<TilableDef, int> current;
        public bool finished;

        public Recipe(RecipeDef recipeDef)
        {
            this.recipeDef = recipeDef;
            finished = false;
            current = new Dictionary<TilableDef, int>();
            foreach (KeyValuePair<TilableDef,int> kv in this.recipeDef.reqs)
            {
                current[kv.Key] = 0;
            }

            WorldUtils.recipes.Add(this);
        }

        public bool IsComplete()
        {
            if (!finished)
            {
                foreach (KeyValuePair<TilableDef, int> kv in recipeDef.reqs)
                {
                    if (current[kv.Key] < recipeDef.reqs[kv.Key])
                    {
                        return false;
                    }
                }

                finished = true;
                WorldUtils.recipes.Remove(this);

                return true;
            }

            return true;
        }
    }

    public class Buildings : Tilable
    {
        private ConnectedTilable _connectedTilable;
        protected Recipe recipe;
        public int work;
        public bool isBlueprint => tilableDef.buildingDef.work == work;

        public Buildings(Vector2Int position, TilableDef tilableDef)
        {
            this.position = position;
            this.tilableDef = tilableDef;
            recipe = new Recipe(tilableDef.recipeDef);
            work = 0;

            if (tilableDef.type == TilableType.BuildingConnected)
            {
                mainGraphic = GraphicInstance.GetNew(
                    tilableDef.graphics,
                    new Color(0.44f, 0.31f, 0.18f, 0.5f), // При збудуванні змініти альфу на 1
                    Res.textures[this.tilableDef.graphics.textureName + "_0"],
                    1
                );
                _connectedTilable = new ConnectedTilable(this);
            }

            addGraphics = new Dictionary<string, GraphicInstance>();

            Tilable tilable = ToolBox.map.GetTilableAt(position, Layer.Plant);

            if (tilable != null && tilable.tilableDef.cuttable)
            {
                tilable.AddOrder(Defs.orders["cut_plants"]);
            }
        }

        public override void UpdateGraphics()
        {
            if (tilableDef.type == TilableType.BuildingConnected)
            {
                _connectedTilable.UpdateGraphics();
            }
        }
    }
}