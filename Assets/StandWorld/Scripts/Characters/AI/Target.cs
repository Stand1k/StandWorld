using System.Collections.Generic;
using System.Xml;
using StandWorld.Entities;
using StandWorld.Game;
using StandWorld.Helpers;
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
                if (current.closest == new Vector2Int(-1, -1))
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
     
    public class Target
    {
        public Entity entity { get; protected set; }
        public Vector2Int position { get; protected set; }
        public Vector2Int closest  { get; protected set; }

        public Target(Entity entity) : this(entity.position)
        {
            this.entity = entity;
        }
        
        public Target(Vector2Int position)
        {
            this.position = position;
            closest = new Vector2Int(-1, -1);
        }
        
        public bool ClosestNeighbour(Vector2Int fromPosition)
        {
            float distance = float.MaxValue;
            TileProperty closestNeigbour = null;

            for (int i = 0; i < 8; i++)
            {
                TileProperty tileProperty = ToolBox.map[position + DirectionUtils.neighbours[i]];

                if (tileProperty != null && !tileProperty.blockPath)
                {
                    float dist = Utils.Distance(fromPosition, position);

                    if (dist < distance)
                    {
                        distance = dist;
                        closestNeigbour = tileProperty;
                    }
                }
            }

            if (closestNeigbour != null)
            {
                closest = closestNeigbour.position;
                return true;
            }

            return false; 
        }
        
        public static Target GetRandomTargetInRange(Vector2Int position, int range = 7)
        {
            Vector2Int targerPosition = new Vector2Int(
                Random.Range(position.x - range, position.x + range),
                Random.Range(position.y - range, position.y + range)
            );

            while (ToolBox.map[targerPosition] == null || ToolBox.map[targerPosition].blockPath || ToolBox.map[targerPosition].reserved)
            {
                targerPosition = new Vector2Int(
                    Random.Range(position.x - range, position.x + range),
                    Random.Range(position.y - range, position.y + range)
                );
            }

            return new Target(targerPosition);
        }
     
    }
}
