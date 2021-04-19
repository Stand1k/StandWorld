using System.Collections.Generic;
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
        protected virtual void DeleteTilable(Vector2Int position)
        {
            positions.Remove(position);
        }

        public void Add(Vector2Int position)
        {
            AddTilable(position);
        }

        public void Delete(Vector2Int position)
        {
            DeleteTilable(position);
        }

        public void Add(RectI rect)
        {
            foreach (Vector2Int position in rect)
            {
                AddTilable(position);
            }
        }

        public void Delete(RectI rect)
        {
            foreach (Vector2Int position in rect)
            {
                DeleteTilable(position);
            }
        }
    }
}
