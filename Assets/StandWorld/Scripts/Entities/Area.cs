using System.Collections.Generic;
using StandWorld.Definitions;
using StandWorld.Game;
using StandWorld.Helpers;
using UnityEngine;

namespace StandWorld.Entities
{
    public abstract class Area 
    {
        public HashSet<Vector2Int> positions { get; protected set; }
        public Color color { get; protected set; }

        public Area()
        {
            positions = new HashSet<Vector2Int>();
        }

        protected virtual void AddTilable(Vector2Int position)
        {
            positions.Add(position);
        }
        protected virtual void DelTilable(Vector2Int position)
        {
            positions.Remove(position);
        }

        public void Add(Vector2Int position)
        {
            AddTilable(position);
        }

        public void Del(Vector2Int position)
        {
            DelTilable(position);
        }

        public void Add(RectI rect)
        {
            foreach (Vector2Int position in rect)
            {
                AddTilable(position);
            }
        }

        public void Del(RectI rect)
        {
            foreach (Vector2Int position in rect)
            {
                DelTilable(position);
            }
        }
    }

    public class GrowArea : Area
    {
        public static List<GrowArea> growAreas = new List<GrowArea>();
        public TilableDef def { get; protected set; }

        public GrowArea(TilableDef def)
        {
            this.def = def;
            color = new Color(0.76f, 1f, 0f, 0.5f);
            growAreas.Add(this);
        }

        protected override void AddTilable(Vector2Int position)
        {
            ToolBox.map.Spawn(
                position,
                new Field(position, Defs.empty, this)
                );
            base.AddTilable(position);
        }

        protected override void DelTilable(Vector2Int position)
        {
            base.DelTilable(position);
        }
    }
}
