using UnityEngine;

namespace StandWorld.Helpers
{
    public readonly struct Wave
    {
        public readonly float seed;
        public readonly float frequency;
        public readonly float amplitude;

        public Wave(float seed, float frequency, float amplitude)
        {
            this.seed = seed;
            this.frequency = frequency;
            this.amplitude = amplitude;
        }
    }

    public static class NoiseMap
    {
        public static Wave[] GroundWave(float seed)
        {
            Wave[] waves = new Wave[3];
            waves[0] = new Wave(seed * 21, 1, 0.5f);
            waves[1] = new Wave(seed * 666, .5f, 1f);
            waves[2] = new Wave(seed * 2121, .25f, 2f);

            return waves;
        }

        public static float[] GenerateNoiseMap(Vector2Int size, float scale, Wave[] waves)
        {
            float[] noiseMap = new float[size.x * size.y];

            for (int x = 0; x < size.x; x++)
            {
                for (int y = 0; y < size.y; y++)
                {
                    float sx = x / scale;
                    float sy = y / scale;
                    float noise = 0f;
                    float norm = 0f;

                    foreach (Wave w in waves)
                    {
                        noise += w.amplitude * Mathf.PerlinNoise(sx * w.frequency + w.seed, sy * w.frequency + w.seed);
                        norm += w.amplitude;
                    }

                    noise /= norm;
                    noiseMap[x + y * size.x] = noise;
                }
            }

            return noiseMap;
        }
    }

}
