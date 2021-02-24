using UnityEngine;
using System.Collections.Generic;
using StandWorld.Definitions;
using StandWorld.Entities;

namespace StandWorld.World
{
    public class Tile
    {
        public Vector2Int position { get; protected set; }
        
        public Map map { get; protected set; }

        private Dictionary<Layer, Tilable> _tilables;

        public Tile(Vector2Int position, Map map)
        {
            this.position = position;
            this.map = map;
            this._tilables = new Dictionary<Layer, Tilable>();
        }

        public void AddTilable(Tilable tilable)
        {
            if (!_tilables.ContainsKey(tilable.def.layer))
            {
                this._tilables.Add(tilable.def.layer, tilable);
                return;
            }

            throw new System.Exception("[Tile.AddTilable] Спроба додати тайл в зайняту позицію");
        }

        public Tilable GetTilable(Layer layer)
        {
            if (this._tilables.ContainsKey(layer))
            {
                return this._tilables[layer];
            }

            return null;
        }
    }
}