using System.Collections.Generic;
using Priority_Queue;
using StandWorld.Game;
using StandWorld.Helpers;
using UnityEditor;
using UnityEngine;

namespace StandWorld.Characters.AI
{
    public struct PathResult
    {
        public Vector2Int[] path;
        public bool success;

        public PathResult(Vector2Int[] path, bool success)
        {
            this.path = path;
            this.success = success;
        }
    }
    
    public static class Pathfinder 
    {
        public static PathResult GetPath(Vector2Int startPosition, Vector2Int endPosition)
        {
            TileProperty start = ToolBox.map[startPosition];
            TileProperty end = ToolBox.map[endPosition];
            bool success = false;
            Vector2Int[] path = new Vector2Int[0];
            start.parent = start;

            if (!start.blockPath && !end.blockPath)
            {
                SimplePriorityQueue<TileProperty> openSet = new SimplePriorityQueue<TileProperty>();
                HashSet<TileProperty> closedSet = new HashSet<TileProperty>();
                
                openSet.Enqueue(start, start.fCost);

                while (openSet.Count > 0)
                {
                    TileProperty current = openSet.Dequeue();

                    if (current == end)
                    {
                        success = true;
                        break;
                    }

                    closedSet.Add(current);
                    for (int i = 0; i < 8; i++)
                    {
                        TileProperty neighbour = ToolBox.map[current.position + DirectionUtils.neighbours[i]];

                        if (neighbour == null || neighbour.blockPath || closedSet.Contains(neighbour))
                        {
                            continue;
                        }

                        float neighbourCost = current.gCost + GameUtils.Distance(current.position, neighbour.position) + neighbour.pathCost;

                        if (neighbourCost > neighbour.gCost || !openSet.Contains(neighbour))
                        {
                            neighbour.gCost = neighbourCost; 
                            neighbour.hCost = GameUtils.Distance(neighbour.position, end.position);
                            neighbour.parent = current;

                            if (!openSet.Contains(neighbour))
                            {
                                openSet.Enqueue(neighbour, neighbour.fCost);
                            }
                            else
                            {
                                openSet.UpdatePriority(neighbour, neighbour.fCost);
                            }
                        }
                    }
                }
            }

            if (success)
            {
                path = CalcPath(start, end);
                success = path.Length > 0;
            }

            return new PathResult(path, success);
        }

        public static Vector2Int[] CalcPath(TileProperty start, TileProperty end)
        {
            List<Vector2Int> path = new List<Vector2Int>();
            TileProperty current = end;

            while (current != start)
            {
                path.Add(current.position);
                current = current.parent;
            }

            Vector2Int[] result = path.ToArray();
            System.Array.Reverse(result);
            return result;
        }
    }
}
