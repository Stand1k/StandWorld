﻿using UnityEngine;

namespace StandWorld.Helpers
{
    public class Utils
    {
        public static float Distance(Vector2Int a, Vector2Int b)
        {
            if (Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) == 1) 
            {
                return 1f;
            }

            if (Mathf.Abs(a.x - b.x) == 1 && Mathf.Abs(a.y - b.y) == 1)
            {
                return 1.41121356237f;
            }

            return Mathf.Sqrt(
                Mathf.Pow(a.x - (float)b.x, 2) +
                Mathf.Pow(a.y - (float)b.y, 2)				
            );
        }
    }
}