using UnityEngine;

namespace StandWorld.MapGenerator
{
	public static class NoiseMap
    {
	    public static float[] GenerateNoiseMap(Vector2Int size, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
        {
			float[] noiseMap = new float[size.x * size.y];

			System.Random prng = new System.Random (seed);
			Vector2[] octaveOffsets = new Vector2[octaves];
			for (int i = 0; i < octaves; i++)
			{
				float offsetX = prng.Next (-100000, 100000) + offset.y;
				float offsetY = prng.Next (-100000, 100000) + offset.x;
				octaveOffsets [i] = new Vector2 (offsetX, offsetY);
			}

			if (scale <= 0)
			{
				scale = 0.0001f;
			}

			float maxNoiseHeight = float.MinValue;
			float minNoiseHeight = float.MaxValue;

			float halfWidth = size.x / 2f;
			float halfHeight = size.y / 2f;


			for (int y = 0; y < size.y; y++)
			{
				for (int x = 0; x < size.x; x++) 
				{
			
					float amplitude = 1;
					float frequency = 1;
					float noiseHeight = 0;

					for (int i = 0; i < octaves; i++) 
					{
						float sampleX = (x-halfWidth) / scale * frequency + octaveOffsets[i].x;
						float sampleY = (y-halfHeight) / scale * frequency + octaveOffsets[i].y;

						float perlinValue = Mathf.PerlinNoise (sampleX, sampleY) * 2 - 1;
						noiseHeight += perlinValue * amplitude;

						amplitude *= persistance;
						frequency *= lacunarity;
					}

					if (noiseHeight > maxNoiseHeight)
					{
						maxNoiseHeight = noiseHeight;
					} 
					else if (noiseHeight < minNoiseHeight)
					{
						minNoiseHeight = noiseHeight;
					}
					
					noiseMap [y + x * size.x] = noiseHeight;
				}
			}

			for (int y = 0; y < size.y; y++) 
			{
				for (int x = 0; x < size.x; x++) 
				{
					noiseMap [y + x * size.x] = Mathf.InverseLerp (minNoiseHeight, maxNoiseHeight, noiseMap [y + x * size.x]);
				}
			}

			return noiseMap;
		} 
    }
}
