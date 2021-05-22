using StandWorld.Definitions;
using StandWorld.Game;
using StandWorld.Helpers;
using StandWorld.Visuals;
using UnityEngine;

namespace StandWorld.Entities
{
    /// <summary>
    /// Перевіряє сусідні тайли і встановлює відповідний спрайт данному тайлу
    /// </summary>
    public class ConnectedTilable
    {
        private readonly bool[] connections;

        private int connectionsInt = -1;
        private bool[] corners { get; set; }
        private bool allCorners { get; set; }
        private Tilable tilable { get; set; }

        public Color color { get; protected set; }

        public ConnectedTilable(Tilable tilable)
        {
            this.tilable = tilable;
            connections = new bool[8];
            connectionsInt = -1;
            allCorners = false;
            corners = new bool[4];
            color = tilable.mainGraphic.color;
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
            Tilable tilable = ToolBox.Instance.map.GetTilableAt(position, this.tilable.tilableDef.layer);

            if (tilable == null || this.tilable.tilableDef != tilable.tilableDef)
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
            
            foreach (int direction in DirectionUtils.cardinals)
            {
                if (connections[direction])
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

            // Оновлює спрайт данному тайлу
            if (connsInt != connectionsInt)
            {
                connectionsInt = connsInt;
                
                if (allCorners)
                {
                    tilable.mainGraphic = GraphicInstance.GetNew(
                        tilable.tilableDef.graphics,
                        color,
                        Res.textures[tilable.tilableDef.graphics.textureName + "_cover"],
                        1
                    );
                    ToolBox.Instance.map.GetTilableAt(tilable.position, Layer.Ground).hidden = true;
                }
                else
                {
                    tilable.mainGraphic = GraphicInstance.GetNew(
                        tilable.tilableDef.graphics,
                        color,
                        Res.textures[tilable.tilableDef.graphics.textureName + "_" + connectionsInt.ToString()],
                        1
                    );
                    
                    if(hasCorner)
                    {
                        tilable.addGraphics.Add("cover",
                            GraphicInstance.GetNew(
                                tilable.tilableDef.graphics,
                                color,
                                Res.textures[tilable.tilableDef.graphics.textureName + "_cover"],
                                2,
                                MeshPool.GetCornersPlane(corners)
                            )
                        );
                    }
                    ToolBox.Instance.map.GetTilableAt(tilable.position, Layer.Ground).hidden = false;
                }
            }
        }
    }
}