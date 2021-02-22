using UnityEngine;
using System.Collections.Generic;
using StandWorld.Definitions;
using StandWorld.Entities;
using UnityEngine.UIElements;

namespace StandWorld.World
{
    public class Map
    {
        public Vector2Int size { get; protected set; }

        private Tile[] _tiles;


        public Map(int width, int height)
        {
            this.size = new Vector2Int(width, height);
            this._tiles = new Tile[width * height];
            
            for (int x = 0; x < this.size.x; x++)
            {
                for (int y = 0; y < this.size.y; y++)
                {
                    this._tiles[x + y * this.size.x] = new Tile(new Vector2Int(x, y), this);
                }
            }
        }

        public void TempEverythingDirt()
        {
            foreach (Tile tile in this)
            {
                tile.AddTilable(new Ground(tile.position, Defs.grounds["dirt"]));
            }
        }
        
        public Tile this[int x, int y]
        {
            get
            {
                if (x >= 0 && y >= 0 && x < this.size.x && y < this.size.y)
                {
                    return this._tiles[x + y * this.size.x];
                }

                return null;
            }
        }

        public Tile this[Vector2Int v]
        {
            get
            {
                return this[v.x, v.y];
            }
        }

        public IEnumerator<Tile> GetEnumerator()
        {
            for (int x = 0; x < this.size.x; x++)
            {
                for (int y = 0; y < this.size.y; y++)
                {
                    yield return this[x, y];
                }
            }
        }

        public override string ToString()
        {
            return "Map(size=" + this.size + ")";
        }
    }
}
