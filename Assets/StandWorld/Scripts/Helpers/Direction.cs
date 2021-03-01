using UnityEngine;

namespace StandWorld.Helpers
{
    public enum Direction
    {
       S, SW, W, NW, N, NE, E, SE
    }

    public static class DirectionUtils
    {
        public static Vector2Int[] neighbours = new Vector2Int[8];

        public static void SetNeighbours()
        {
            neighbours[0] = new Vector2Int(0,-1);
            neighbours[1] = new Vector2Int(-1, -1);
            neighbours[2] = new Vector2Int(-1, 0);
            neighbours[3] = new Vector2Int(-1, 1);
            neighbours[4] = new Vector2Int(0, 1);
            neighbours[5] = new Vector2Int(1, 1);
            neighbours[6] = new Vector2Int(1, 0);
            neighbours[7] = new Vector2Int(1, -1);
        }
    }
}
