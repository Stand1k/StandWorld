using StandWorld.Definitions;
using StandWorld.Game;
using StandWorld.World;
using UnityEngine;

namespace StandWorld.Visuals
{
    public static class DebugRenderer 
    {
        public static void DrawFertility()
        {
            Color color1 = new Color(1, 0, 0, 0.5f);
            Color color2 = new Color(0, 1, 0, 0.5f);
            foreach (Vector2Int v in ToolBox.cameraController.viewRect)
            {
                Gizmos.color = Color.Lerp(color1, color2, ToolBox.map[v].fertility);
                
                Gizmos.DrawCube(
                    new Vector3(v.x + 0.5f, v.y + 0.5f), 
                    Vector3.one
                );
            }
        }
        
        public static void DrawBuckets()
        {
            for (int x = 0; x < ToolBox.map.size.x; x += Settings.BUCKET_SIZE)
            {
                for (int y = 0; y < ToolBox.map.size.y; y += Settings.BUCKET_SIZE)
                {
                    LayerBucketGrid bucket = ToolBox.map.grids[Layer.Ground].GetBucketAt(new Vector2Int(x,y));
                                
                    Gizmos.color =  new Color(0f, 0.91f, 1f, 0.5f);
                                
                    Gizmos.DrawCube(
                        new Vector3(
                            bucket.rect.max.x - (bucket.rect.width - 0.5f) / 2,
                            bucket.rect.max.y - (bucket.rect.height - 0.5f) / 2 
                        ),
                        new Vector3(bucket.rect.width - 0.5f, bucket.rect.height - 0.5f)
                    );
                }
            }
        }

        public static void DrawTiles()
        {
            foreach (Vector2Int v in ToolBox.cameraController.viewRect)
            {
                Gizmos.DrawWireCube(
                    new Vector3(v.x + 0.5f, v.y + 0.5f), 
                    Vector3.one
                );
            }
        }

        public static void DrawNoiseMap()
        {
            foreach (Vector2Int v in ToolBox.map.mapRect)
            {
                float h = ToolBox.map.groundNoiseMap[v.x + v.y * ToolBox.map.size.x];
                Gizmos.color = new Color(h, h, h, 1f);
                Gizmos.DrawCube(
                    new Vector3(v.x + 0.5f, v.y + 0.5f),
                    Vector3.one
                );
            }
        }
    }
}
