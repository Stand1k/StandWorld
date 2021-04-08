using System.Collections.Generic;
using System.Linq;
using StandWorld.Characters;
using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.Game;
using StandWorld.Helpers;
using StandWorld.MapGenerator;
using UnityEngine;

namespace StandWorld.World
{
    public class Map 
    {
        public const int BUCKET_SIZE = Settings.BUCKET_SIZE;
        
        public Vector2Int size { get; protected set; }

        public RectI mapRect;

        public float[] groundNoiseMap { get; protected set; }
    
        public Dictionary<Layer, LayerGrid> grids;
        
        public TileProperty[] tiles { get; protected set; }
        
        public List<BaseCharacter> characters { get; protected set; }
        
        private float noiseScale = Settings.noiseScale;
        private int octaves = Settings.octaves;
        private float persistance = Settings.persistance;
        private float lacunarity = Settings.lacunarity;
        private int seed = Random.Range(100000000, 999999999);
        private Vector2 offset = Settings.offset;
        
        public Map(int width, int height)
        {
            size = new Vector2Int(width, height);
            mapRect = new RectI(new Vector2Int(0, 0), width, height);
            tiles = new TileProperty[width * height];

            foreach (Vector2Int position in mapRect)
            {
                tiles[position.x + position.y * size.y] = new TileProperty(position);
            }

            characters = new List<BaseCharacter>();
            
            grids = new Dictionary<Layer, LayerGrid>();
            grids.Add(Layer.Ground, new GroundGrid(size));
            grids.Add(Layer.Plant, new TilableGrid(size));
            grids.Add(Layer.Mountain, new TilableGrid(size));
            grids.Add(Layer.Stackable, new TilableGrid(size));
            grids.Add(Layer.Helpers, new TilableGrid(size));
        }
        
        public void Spawn(Vector2Int position, Tilable tilable, bool force = false)
        {
            if (force || tilable.def.layer == Layer.Undefined || GetTilableAt(position, tilable.def.layer) == null)
            {
                grids[tilable.def.layer].AddTilable(tilable);
            }
        }

        public void SpawnCharacter(BaseCharacter character)
        {
            characters.Add(character);
        }

        public void UpdateCharacters()
        {
            foreach (BaseCharacter character in characters)
            {
                character.UpdateDraw();
            }
        }
        
        public void BuildAllMeshes()
        {
            foreach (LayerGrid grid in grids.Values)
            {
                grid.BuildStaticMeshes(); 
            }
        }

        public void CheckAllMatrices()
        {
            foreach (LayerGrid grid in grids.Values)
            {
                grid.CheckMatriceUpdates(); 
            }
        }

        public void DrawTilables()
        {
            foreach (LayerGrid grid in grids.Values)
            {
                grid.DrawBuckets(); 
            }
        }

        public Tilable GetTilableAt(Vector2Int position, Layer layer)
        {
            return grids[layer].GetTilableAt(position);
        }

        public IEnumerable<Tilable> GetAllTilablesAt(Vector2Int position)
        {
            foreach (LayerGrid grid in grids.Values)
            {
                Tilable tilable = grid.GetTilableAt(position);
                if (tilable != null)
                {
                    yield return tilable;
                }
            }
        }

        public void UpdateVisibles()
        {
            int i = 0;
            foreach (LayerBucketGrid bucket in grids[Layer.Ground].buckets)
            {
                bool bucketVisible = bucket.CalcVisible();
                foreach (LayerGrid grid in grids.Values)
                {
                    if (grid.layer != Layer.Ground)
                    {
                        grid.buckets[i].SetVisible(bucketVisible);
                    }
                }
                i++;
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

        public void TempMapGen()
        {
            groundNoiseMap = NoiseMap.GenerateNoiseMap(size, 613864505, noiseScale, octaves, persistance, lacunarity, offset);
            Debug.Log("Seed: " + seed.ToString());

            foreach (Vector2Int position in mapRect)
            {
                Spawn(
                    position,
                    new Ground(
                        position,
                        // Повертає TilableDef який вказує який тип потрібно відображати на цьому тайлі
                        //Порівнює карту шумів і всі типи тайлів і взалежності який підходить такий й повертає
                        Ground.GroundByHeight(groundNoiseMap[position.x + position.y * size.x])
                    )
                );
                
                if (grids[Layer.Ground].GetTilableAt(position).def.uID == "rock")
                {
                    Spawn(
                        position,
                        new Mountain(position, Defs.mountains["mountain"])
                        );
                }
                
                //Перевіряє родючість і якщо вона підходить, то там спавниться певна рослина
                //яка в свою чергу має вимоги до родючості
                if (this[position].fertility > 0f && !this[position].blockPlant)
                {
                    foreach (TilableDef tilableDef in Defs.plants.Values)
                    {
                        if (this[position].fertility >= tilableDef.plantDef.minFertility &&
                            Random.value <= tilableDef.plantDef.probability)
                        {
                            Spawn(
                                position,
                                new Plant(position, tilableDef, true)
                                );
                            break;
                        }
                    }
                }
            }

            foreach (LayerBucketGrid bucket in grids[Layer.Mountain].buckets)
            {
                bool changed = false;
                foreach (Tilable tilable in bucket.tilables)
                {
                    if (tilable != null)
                    {
                        tilable.UpdateGraphics();
                        changed = true;
                    }
                }

                if (changed)
                {
                    bucket.rebuildMatrices = true;
                }
            }
            
            /*foreach(Vector2Int position in new RectI(new Vector2Int(30,30),10,10))
            {
                if (this[position].blockStackable == false)
                {
                    Spawn(position, new Stackable(
                        position,
                        Defs.stackables["logs"],
                        Random.Range(1, Defs.stackables["logs"].maxStack)
                    ));
                }
            }*/
            
        }
        
        public TileProperty this[Vector2Int position]
        {
            get
            {
                if (position.x >= 0 && position.y >= 0 && position.x < size.x && position.y < size.y)
                {
                    return tiles[position.x + position.y * size.y];
                }

                return tiles[0];
            }
        }

        public override string ToString()
        {
            return "Map(size=" + size + ")";
        }

    }
}
