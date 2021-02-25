using System.Collections.Generic;
using System.Linq;
using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.Helpers;
using UnityEngine;

namespace StandWorld.World
{
    public class Map
    {
        public const int REGION_SIZE = 25;

        public Vector2Int size { get; protected set; }

        public RectI mapRect;

        public MapRegion[] regions
        {
            get
            {
                return _regions;
            }
        }
    
        private Tile[] _tiles;

        private MapRegion[] _regions;
        private Dictionary<int, int> _regionByPosition;
        
        public Map(int width, int height)
        {
            size = new Vector2Int(width, height);
            _tiles = new Tile[width * height];
            mapRect = new RectI(new Vector2Int(0, 0), width, height);
            
            foreach (Vector2Int v in mapRect)
            {
                _tiles[v.x + v.y * size.x] = new Tile(v, this);
            }
            
            SetRegions();
        }

        public void SetRegions()
        {
            
            int _regionLength = (
                Mathf.CeilToInt(size.x / REGION_SIZE) *
                Mathf.CeilToInt(size.y / REGION_SIZE)
            );
            
            _regions = new MapRegion[_regionLength];
            _regionByPosition = new Dictionary<int, int>();

            int i = 0;

            for (int x = 0; x < size.x; x += REGION_SIZE)
            {
                for (int y = 0; y < size.y; y += REGION_SIZE)
                {
                    RectI sectionRect = new RectI(
                        new Vector2Int(x, y), 
                        REGION_SIZE, 
                        REGION_SIZE
                    );
                    sectionRect.CLip(mapRect);
                    _regions[i] = new MapRegion(i, sectionRect, this);
                   
                    foreach (Vector2Int v in sectionRect)
                    {
                        int key = v.x + v.y * size.x;
                        _regionByPosition.Add(key, i);
                    }
                    i++;
                }
            }

        }
        
        //Використовується чисто для теста 
        public void TempMapGen()
        {
            foreach (Tile tile in this)
            {
                if (tile.position.x == 0 || tile.position.y == 0 || tile.position.x == this.size.x - 1 ||
                    tile.position.y == this.size.y - 1)
                {
                    tile.AddTilable(new Ground(tile.position, Defs.grounds["water"]));
                }
                else
                {
                    {
                        tile.AddTilable(new Ground(tile.position, Defs.grounds["dirt"]));
                    }
                }

                if (Random.value > .8f)
                {
                    tile.AddTilable(new Plant(tile.position, Defs.plants["grass"]));
                }
            }
        }
        
        public Tile this[int x, int y]
        {
            get
            {
                if (x >= 0 && y >= 0 && x < size.x && y < size.y)
                {
                    return _tiles[x + y * size.x];
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
            foreach (Vector2Int v in mapRect)
            {
                yield return this[v];
            }
        }

        public override string ToString()
        {
            return "Map(size=" + size + ")";
        }
    }
}
