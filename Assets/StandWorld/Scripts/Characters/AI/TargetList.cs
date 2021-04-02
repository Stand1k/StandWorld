using System.Collections.Generic;
using StandWorld.Entities;
using StandWorld.Game;
using UnityEngine;

namespace StandWorld.Characters.AI
{
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

        public TargetList(Entity entity)
        {
            Enqueue(entity);
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

        public TargetList(List<Entity> entities)
        {
            foreach (Entity entity in entities)
            {
                Enqueue(entity);
            }

            Next();
        }

        public void Enqueue(Target target)
        {
            ToolBox.map[target.position].reserved = true;
            targets.Enqueue(target);
        }

        public void Enqueue(Entity entity)
        {
            ToolBox.map[entity.position].reserved = true;
            targets.Enqueue(new Target(entity));
        }

        public void Enqueue(Vector2Int position)
        {
            ToolBox.map[position].reserved = true;
            targets.Enqueue(new Target(position));
        }

        public void Next()
        {
            if (current != null)
            {
                ToolBox.map[current.position].reserved = false;
            }

            current = targets.Dequeue();
        }
    }
}