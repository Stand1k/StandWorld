using UnityEngine;

namespace StandWorld
{
    public static class Settings
    {
        public const int BUCKET_SIZE = 32;
        public const int TICKS_PER_DAY = 3000;
        public const int DAYS_PER_SEASON = 14;
        public const int SEASONS_PER_YEAR = 4;
        public const int TICKS_PER_SEASON = TICKS_PER_DAY * DAYS_PER_SEASON;
        public const int TICKS_PER_YEAR = SEASONS_PER_YEAR * TICKS_PER_SEASON;

        public const bool DEBUG = true;
        
        
        public static float noiseScale;
        public static int octaves;
        public static  float persistance;
        public static float lacunarity;
        public static int seed;
        public static Vector2 offset;
    }
}
