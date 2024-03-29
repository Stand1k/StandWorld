﻿using System.Collections.Generic;
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
        public float[] plantsNoiseMap { get; protected set; }
        public float[] randomGrowNoiseMap { get; protected set; }

        public Dictionary<Layer, LayerGrid> grids;

        public TileProperty[] tiles { get; protected set; }

        public List<BaseCharacter> characters { get; protected set; }

        private float noiseScale = Settings.noiseScale;
        private int octaves = Settings.octaves;
        private float persistance = Settings.persistance;
        private float lacunarity = Settings.lacunarity;
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
            grids.Add(Layer.Building, new TilableGrid(size));
            grids.Add(Layer.Stackable, new TilableGrid(size));
            grids.Add(Layer.Orders, new TilableGrid(size));
            grids.Add(Layer.FX, new TilableGrid(size));
            grids.Add(Layer.Helpers, new TilableGrid(size));
        }

        public void Spawn(Vector2Int position, Tilable tilable, bool force = false)
        {
            if (force || tilable.tilableDef.layer == Layer.Undefined ||
                GetTilableAt(position, tilable.tilableDef.layer) == null)
            {
                grids[tilable.tilableDef.layer].AddTilable(tilable);
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

        public void UpdateConnectedMountains()
        {
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
        }

        public void UpdateConnectedBuildings()
        {
            foreach (LayerBucketGrid bucket in grids[Layer.Building].buckets)
            {
                bool changed = false;
                foreach (Tilable tilable in bucket.tilables)
                {
                    if (tilable != null && tilable.tilableDef.type == TilableType.BuildingConnected)
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
        }

        public void TempMapGen()
        {
            groundNoiseMap =
                NoiseMap.GenerateNoiseMap(size, Settings.seed, noiseScale, octaves, persistance, lacunarity, offset);
            plantsNoiseMap =
                NoiseMap.GenerateNoiseMap(size, Settings.seed, 12, 5, 1, 5, offset);
            randomGrowNoiseMap =
                NoiseMap.GenerateNoiseMap(size, Settings.seed, 2, 2, 1, 1.18f, offset);

            foreach (Vector2Int position in mapRect)
            {
                var randomGrowNoiseMapValue = randomGrowNoiseMap[position.x + position.y * size.x];
                var groundNoiseMapValue = groundNoiseMap[position.x + position.y * size.x];
                var plantsNoiseMapValue = plantsNoiseMap[position.x + position.y * size.x];
                
                Spawn(
                    position,
                    new Ground(
                        position,
                        // Повертає TilableDef який вказує який тип потрібно відображати на цьому тайлі
                        //Порівнює карту шумів і всі типи тайлів і взалежності який підходить такий й повертає
                        Ground.GroundByHeight(groundNoiseMapValue)
                    )
                );

                if (grids[Layer.Ground].GetTilableAt(position).tilableDef.uId == "rock")
                {
                    if (groundNoiseMapValue >= 0.62f)
                    {

                        Spawn(
                            position,
                            new Mountain(position, Defs.mountains["mountain"])
                        );
                    }
                }

                //Перевіряє родючість і якщо вона підходить, то там спавниться певна рослина
                //яка в свою чергу має вимоги до родючості
                if (this[position].fertility > 0f && !this[position].blockPlant)
                {
                    foreach (TilableDef tilableDef in Defs.plants.Values)
                    {
                        if (this[position].fertility >= tilableDef.plantDef.minFertility &&
                            plantsNoiseMapValue <= tilableDef.plantDef.probability )
                        {
                            Spawn(
                                position,
                                new Plant(position, tilableDef, true, randomGrowNoiseMapValue)
                            );
                            break;
                        }
                    }
                }
            }

            UpdateConnectedMountains();
        }

        public TileProperty this[Vector2Int position]
        {
            get
            {
                if (position.x >= 0 && position.y >= 0 && position.x < size.x && position.y < size.y)
                {
                    return tiles[position.x + position.y * size.y];
                }

                return null;
            }
        }

        public override string ToString()
        {
            return "Map(size=" + size + ")";
        }
    }
}