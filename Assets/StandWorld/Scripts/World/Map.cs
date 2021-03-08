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
        public const int REGION_SIZE = Settings.REGION_SIZE;
        
        public float noiseScale = Settings.noiseScale;

        public int octaves = Settings.octaves;
        public float persistance = Settings.persistance;
        public float lacunarity = Settings.lacunarity;

        public int seed = Settings.seed;
        public Vector2 offset = Settings.offset;

        public Vector2Int size { get; protected set; }

        public RectI mapRect;

        private Tile[] _tiles;

        public float[] groundNoiseMap { get; protected set; }

        public GroundGrid groundGrid;
        public PlantGrid plantGrid;
        
        public Map(int width, int height)
        {
            size = new Vector2Int(width, height);
            mapRect = new RectI(new Vector2Int(0, 0), width, height);
           
            groundGrid = new GroundGrid(size);
            plantGrid = new PlantGrid(size);
            
            /*foreach (Vector2Int v in mapRect)
            {
                _tiles[v.x + v.y * size.x] = new Tile(v, this);
            }*/

            SetRegions();
        }

        public void SetRegions()
        {
            int _regionLength = (
                Mathf.CeilToInt(size.x / REGION_SIZE) *
                Mathf.CeilToInt(size.y / REGION_SIZE)
            );
            

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
                    sectionRect.Clip(mapRect);
                    i++;
                }
            }
        }

        public IEnumerable<LayerGrid> GetAllGrids()
        {
            yield return groundGrid;
            yield return plantGrid;
        }

        public IEnumerable<Tilable> GetAllTilablesAt(Vector2Int position)
        {
            foreach (LayerGrid grid in GetAllGrids())
            {
                Tilable tilable = grid.GetTilableAt(position);
                if (tilable != null)
                {
                    yield return tilable;
                }
            }
        }

        public float GetFertilityAt(Vector2Int position)
        {
            float fertility = 1f;

            foreach (Tilable tilable in GetAllTilablesAt(position))
            {
                if (tilable.def.fertility == 0f)
                {
                    return 0f;
                }

                fertility *= tilable.def.fertility;
            }

            return fertility;
        }

        //Використовується чисто для теста 
        public void TempMapGen()
        {
            groundNoiseMap = NoiseMap.GenerateNoiseMap(size, seed, noiseScale, octaves, persistance, lacunarity, offset);

            foreach (Vector2Int position in mapRect)
            {
                groundGrid.AddTilable(
                    new Ground(
                        position,
                        // Повертає TilableDef який вказує який тип потрібно відображати на цьому тайлі
                        //Порівнює карту шумів і всі типи тайлів і взалежності який підходить такий й повертає
                        Ground.GroundByHeight(groundNoiseMap[position.x + position.y * size.x]) 
                    )
                );
                
                //Перевіряє родючість і якщо вона підходить, то там спавниться певна рослина
                //яка в свою чергу має вимоги до родючості
                float _tileFertility = GetFertilityAt(position);
                if (_tileFertility > 0f)
                {
                    foreach (TilableDef tilableDef in Defs.plants.Values)
                    {
                        if (_tileFertility >= tilableDef.plantDef.minFertility &&
                            Random.value <= tilableDef.plantDef.probability)
                        {
                            plantGrid.AddTilable(
                                new Plant(position, tilableDef, true)
                                );
                            break;
                        }
                    }
                }
            }
        }

        /*public Tile this[int x, int y]
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
        }*/

        public override string ToString()
        {
            return "Map(size=" + size + ")";
        }

    }
}
