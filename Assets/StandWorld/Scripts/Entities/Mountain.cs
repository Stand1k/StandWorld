using System;
using System.Collections.Generic;
using StandWorld.Definitions;
using StandWorld.Helpers;
using StandWorld.Visuals;
using UnityEngine;

namespace StandWorld.Entities
{
    public class ConnectedTilable
    {
        public bool[] connections = new bool[8];
        
        public int connectionsInt = -1;
        public bool[] corners { get; protected set; }
        public bool allCorners { get; protected set; }
        public  Tilable tilable { get; protected set; }

        public ConnectedTilable(Tilable tilable)
        {
            this.tilable = tilable;
            connections = new bool[8];
            connectionsInt = -1;
            allCorners = false;
            corners = new bool[4];
        }

        private void SetLinks()
        {
            for (int i = 0; i < 8; i++)
            {
                connections[i] = HasLink(tilable.position + DirectionUtils.neighbours[i]);
            }
        }

        private bool HasLink(Vector2Int position)
        {
            Tilable tilable = ToolBox.map.GetTilableAt(position, this.tilable.def.layer);

            if (tilable == null || this.tilable.def != tilable.def)
            {
                return false;
            }

            return true;
        }

        public void UpdateGraphics()
        {
            int connsInt = 0;
            SetLinks();
            int i = 0;
            
            foreach (int diretion in DirectionUtils.cardinals)
            {
                if (connections[diretion])
                {
                    connsInt += DirectionUtils.connections[i];
                }

                i++;
            }

            i = 0;
            allCorners = true;
            bool hasCorner = false;
            foreach (int direction in DirectionUtils.corners)
            {
                if (
                    (i != 0 || connections[direction] && (connections[(int) Direction.W] && connections[(int) Direction.S])) &&
                    (i != 1 || connections[direction] && (connections[(int) Direction.W] && connections[(int) Direction.N])) &&
                    (i != 2 || connections[direction] && (connections[(int) Direction.E] && connections[(int) Direction.N])) &&
                    (i != 3 || connections[direction] && (connections[(int) Direction.E] && connections[(int) Direction.S]))
                )
                {
                    corners[i] = true;
                    hasCorner = true;
                }
                else
                {
                    corners[i] = false;
                    allCorners = false;
                }
                i++;
            }

            if (connsInt != connectionsInt)
            {
                connectionsInt = connsInt;
                
                if (allCorners)
                {
                    tilable.mainGraphic = GraphicInstance.GetNew(
                        tilable.def.graphics,
                        default(Color),
                        Res.textures[tilable.def.graphics.textureName + "_cover"],
                        1
                    );
                    ToolBox.map.GetTilableAt(tilable.position, Layer.Ground).hidden = true;
                }
                else
                {
                    tilable.mainGraphic = GraphicInstance.GetNew(
                        tilable.def.graphics,
                        default(Color),
                        Res.textures[tilable.def.graphics.textureName + "_" + connectionsInt.ToString()],
                        1
                    );
                    
                    if(hasCorner)
                    {
                        tilable.addGraphics.Add("cover",
                            GraphicInstance.GetNew(
                                tilable.def.graphics,
                                default(Color),
                                Res.textures[tilable.def.graphics.textureName + "_cover"],
                                2,
                                MeshPool.GetCornersPlane(corners)
                            )
                        );
                    }
                    ToolBox.map.GetTilableAt(tilable.position, Layer.Ground).hidden = false;
                }
            }
        }
    }
    
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
