using System.Collections.Generic;
using StandWorld.Definitions;
using StandWorld.Game;
using StandWorld.Helpers;
using StandWorld.Visuals;
using StandWorld.World;
using UnityEngine;

namespace StandWorld.Entities
{
    public class Plant : Tilable
    {
        private Color _leafColor;
        private Color _woodColor;
        private readonly float _lifetime;
        private readonly float _ticksPerState;
        private readonly float _sizePerState;
        private int _currentState;

        public int state => _currentState;

        protected bool cutOrdered;

        public Plant(Vector2Int position, TilableDef tilableDef, bool randomGrow = false)
        {
            addGraphics = new Dictionary<string, GraphicInstance>();
            this.position = position;
            this.tilableDef = tilableDef;
            _lifetime = this.tilableDef.plantDef.lifetime * Settings.TICKS_PER_DAY;
            _ticksPerState = _lifetime / this.tilableDef.plantDef.states;
            _sizePerState = 1f / this.tilableDef.plantDef.states;

            if (randomGrow)
            {
                ticks = Random.Range(0, (int) (_lifetime - _ticksPerState));
            }
            else
            {
                ticks = 0;
                _currentState = 1;
            }

            GetState();
            UpdateGraphics();
            ToolBox.tick.toAdd.Enqueue(Update);
        }

        public override void UpdateGraphics()
        {
            if (tilableDef.type == TilableType.Grass)
            {
                _leafColor = Defs.colorPallets["cols_leafsGreen"].GetRandom();
                mainGraphic = GraphicInstance.GetNew(tilableDef.graphics, _leafColor);
            }
            else if (tilableDef.type == TilableType.Tree)
            {
                _leafColor = Defs.colorPallets["cols_leafsGreen"].GetRandom();
                _woodColor = Defs.colorPallets["cols_wood"].GetRandom();
                mainGraphic = GraphicInstance.GetNew(
                    tilableDef.graphics,
                    _woodColor,
                    Res.textures[tilableDef.graphics.textureName + "_base"],
                    1
                );

                if (addGraphics.ContainsKey("leafs"))
                {
                    addGraphics["leafs"] = GraphicInstance.GetNew(
                        tilableDef.graphics,
                        _leafColor,
                        Res.textures[tilableDef.graphics.textureName + "_leafs"],
                        2
                    );
                }
                else
                {
                    addGraphics.Add("leafs",
                        GraphicInstance.GetNew(
                            tilableDef.graphics,
                            _leafColor,
                            Res.textures[tilableDef.graphics.textureName + "_leafs"],
                            2
                        )
                    );
                }
            }
            else
            {
                mainGraphic = GraphicInstance.GetNew(tilableDef.graphics);
            }
        }

        private void GetState()
        {
            int state = Mathf.CeilToInt(ticks / _ticksPerState);

            if (state > tilableDef.plantDef.states)
            {
                state = tilableDef.plantDef.states;
            }

            if (state != _currentState)
            {
                _currentState = state;
                scale = new Vector3(
                    _currentState * _sizePerState,
                    _currentState * _sizePerState,
                    1
                );

                if (bucket != null)
                {
                    bucket.rebuildMatrices = true;
                }
            }
        }

        public void OrderToCut()
        {
            cutOrdered = true;
        }

        public void Update()
        {
            ticks++;
            GetState();
            if (ticks >= _lifetime)
            {
                Destroy();
            }
        }

        public override void AddOrder(MenuOrderDef def)
        {
            base.AddOrder(def);
            WorldUtils.cutOrdered.Add(this);
        }

        public override void Destroy()
        {
            ToolBox.tick.toDel.Enqueue(Update);
            base.Destroy();
        }

        public void Cut()
        {
            if (WorldUtils.cutOrdered.Contains(this))
            {
                WorldUtils.cutOrdered.Remove(this);
            }

            int qtyLoot = Defs.stackables["logs"].maxStack / ((tilableDef.plantDef.states + 1) - _currentState);

            if (tilableDef.type == TilableType.Tree)
            {
                ToolBox.map.Spawn(position, new Stackable
                (
                    position,
                    Defs.stackables["logs"],
                    qtyLoot
                ));
            }

            Destroy();
        }

        public override void ClearOrder()
        {
            if (WorldUtils.cutOrdered.Contains(this))
            {
                WorldUtils.cutOrdered.Remove(this);
            }
            
            base.ClearOrder();
        }
    }
}