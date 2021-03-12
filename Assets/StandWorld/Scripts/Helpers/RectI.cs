﻿using System.Collections.Generic;
using UnityEngine;

namespace StandWorld.Helpers
{
    //Допоміжний клас який представляє в чотирикутнику(початок чотирикутника і кінець) 
    //як наш світ так і наші регіони(чанки)
    
    [System.Serializable]
    public struct RectI
    {
        public Vector2Int min;
        public Vector2Int max;
        public Vector2Int size => new Vector2Int(width, height);

        public int width => max.x - min.x;

        public int height => max.y - min.y;

        public int area => width * height;

        public RectI(Vector2Int min, Vector2Int max)
        {
            this.min = min;
            this.max = max;
        } 
        
        public RectI(Vector2Int min, int width, int height)
        {
            this.min = min;
            max = new Vector2Int(this.min.x + width, this.min.y + height);
        }

        public IEnumerator<Vector2Int> GetEnumerator()
        {
            for (int x = min.x; x < max.x; x++)
            {
                for (int y = max.y - 1; y >= min.y; y--)
                {
                    yield return new Vector2Int(x, y);
                }
            }
        }
        
        //Порівнює величину чотирикутників і у випадку різниці мініє свій розмір
        public void Clip(RectI other)
        {
            if (min.x < other.min.x)
            {
                min.x = other.min.x;
            }
            
            if (max.x > other.max.x)
            {
                max.x = other.max.x;
            }
            
            if (min.y < other.min.y)
            {
                min.y = other.min.y;
            }
            
            if (max.y > other.max.y)
            {
                max.y = other.max.y;
            }
        }

        public bool Contains(Vector2Int v)
        {
            return (
                v.x >= min.x &&
                v.y >= min.y &&
                v.x < max.x &&
                v.y < max.y
                );
        }

        public override int GetHashCode()
        {
            return min.x + min.y * width + max.x * height + max.y;
        }

        public override string ToString()
        {
            return "RectI(min = " + min + ", max=" + max +", size = "+ size +", area =" + area + ")";
        }
    }
}