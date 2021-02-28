using System;
using System.Collections.Generic;
using StandWorld.Definitions;
using StandWorld.Entities;
using UnityEngine;

namespace StandWorld.World
{
    public class Tile
    {
        public Vector2Int position { get; protected set; }
        
        public Map map { get; protected set; }

        public float fertility
        {
            get
            {
                float _fertility = 1f;
                foreach (Tilable tilable in GetAllTilables())
                {
                    if (tilable.def.fertility == 0f)
                    {
                        return 0f;
                    }

                    _fertility *= tilable.def.fertility;
                }

                return _fertility;
            }
        }

        private Dictionary<Layer, Tilable> _tilables;

        public Tile(Vector2Int position, Map map)
        {
            this.position = position;
            this.map = map;
            _tilables = new Dictionary<Layer, Tilable>();
        }
        
        //Отримати всі Tilable обєкти в цьому тайлі
        public IEnumerable<Tilable> GetAllTilables()
        {
            foreach (Tilable tilable in _tilables.Values)
            {
                yield return tilable;
            }
        }

        public void AddTilable(Tilable tilable)
        {
            if (!_tilables.ContainsKey(tilable.def.layer))
            {
                _tilables.Add(tilable.def.layer, tilable);
                return;
            }

            throw new Exception("[Tile.AddTilable] Спроба додати тайл в зайняту позицію");
        }

        public Tilable GetTilable(Layer layer)
        {
            if (_tilables.ContainsKey(layer))
            {
                return _tilables[layer];
            }

            return null;
        }
    }
}