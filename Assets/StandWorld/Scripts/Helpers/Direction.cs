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
        public static int[] cardinals = new int[4];
        public static int[] corners = new int[4];
        public static int[] connections = new int[4]; // 4-bit маска

        public static void SetNeighbours()
        {
            /*
             ↖  ↑  ↗
             ←     →
             ↙  ↓  ↘
             */
            
            neighbours[0] = new Vector2Int(0,-1);    // S  ↓
            neighbours[1] = new Vector2Int(-1, -1);  // SW ↙
            neighbours[2] = new Vector2Int(-1, 0);   // W  ←
            neighbours[3] = new Vector2Int(-1, 1);   // NW ↖  
            neighbours[4] = new Vector2Int(0, 1);    // N  ↑
            neighbours[5] = new Vector2Int(1, 1);    // NE ↗
            neighbours[6] = new Vector2Int(1, 0);    // E  →
            neighbours[7] = new Vector2Int(1, -1);   // SE ↘
            
            cardinals[0] = (int) Direction.N; // ↑
            cardinals[1] = (int) Direction.W; // ←
            cardinals[2] = (int) Direction.E; // →
            cardinals[3] = (int) Direction.S; // ↓

            connections[0] = 1; 
            connections[1] = 2; 
            connections[2] = 4; 
            connections[3] = 8; 
            
            corners[0] = (int) Direction.SW; // ↙
            corners[1] = (int) Direction.NW; // ↖
            corners[2] = (int) Direction.NE; // ↗
            corners[3] = (int) Direction.SE; // ↘
        }
    }
}
