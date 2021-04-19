using UnityEngine;

namespace StandWorld.Game
{
    public static class Settings
    {
        public const int BUCKET_SIZE = 31;
        public const int TICKS_PER_DAY = 15000;
        public const int DAYS_PER_SEASON = 30;
        public const int SEASONS_PER_YEAR = 4;
        public const int TICKS_PER_SEASON = TICKS_PER_DAY * DAYS_PER_SEASON;
        public const int TICKS_PER_YEAR = SEASONS_PER_YEAR * TICKS_PER_SEASON;

        public const int EYE_COUNT = 1;
        public const int HAIR_COUNT = 1;
        public const int CLOUTHES_COUNT = 2;
        
        public static float noiseScale;
        public static int octaves;
        public static  float persistance;
        public static float lacunarity;
        public static int seed;
        public static Vector2 offset;
    }
}
