using System.Collections.Generic;
using StandWorld.Entities;
using StandWorld.Game;
using UnityEngine;

namespace StandWorld.Characters.AI
{
    public enum TargetType
    {
        None,
        Tile,
        Adjacent,
    }

    public class TargetList
    {
        public Queue<Target> targets = new Queue<Target>();
        public Target current { get; protected set; }

        public Vector2Int currentPosition
        {
            get
            {
                if (current.closest != new Vector2Int(-1, -1))
                {
                    return current.closest;
                }

                return current.position;
            }
        }

        public TargetList(Tilable tilable)
        {
            Enqueue(tilable);
            Next();
        }

        public TargetList(Target target)
        {
            Enqueue(target);
            Next();
        }

        public TargetList(Vector2Int position)
        {
            Enqueue(position);
            Next();
        }

        public TargetList(List<Tilable> tilables)
        {
            foreach (Tilable tilable in tilables)
            {
                Enqueue(tilable);
            }

            Next();
        }

        public void Enqueue(Target target)
        {
            ToolBox.map[target.position].reserved = true;
            targets.Enqueue(target);
        }

        public void Enqueue(Tilable tilable)
        {
            ToolBox.map[tilable.position].reserved = true;
            targets.Enqueue(new Target(tilable));
        }

        public void Enqueue(Vector2Int position)
        {
            ToolBox.map[position].reserved = true;
            targets.Enqueue(new Target(position));
        }

        public void Free()
        {
            if (current != null)
            {
                ToolBox.map[current.position].reserved = false;
            }
        }

        public void FreeAll()
        {
            while (targets.Count != 0)
            {
                Target target = targets.Dequeue();
                ToolBox.map[target.position].reserved = false;
            }
        }

        public void Next()
        {
            Free();
            current = targets.Dequeue();
        }

        public List<Target> ToList()
        {
            return new List<Target>(targets);
        }
    }
}