using StandWorld.Characters;
using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.Game;
using StandWorld.World;
using UnityEngine;

namespace StandWorld.Visuals
{
    public static class DebugRenderer 
    {
        public static void DrawCurrentPath(CharacterMovement movement)
        {
            if (movement.destination != movement.position)
            {
                Gizmos.color = Color.blue;
                Vector2Int[] path = movement.path.ToArray();

                for (int i = 0; i < path.Length; i++)
                {
                    if (i == 0)
                    {
                        Gizmos.DrawLine(
                            new Vector3(movement.visualPosition.x + 0.5f, movement.visualPosition.y + 0.5f),
                            new Vector3(path[i].x + 0.5f, path[i].y + 0.5f)
                        );
                    }
                    else
                    {
                        Gizmos.DrawLine(
                            new Vector3(path[i - 1].x + 0.5f, path[i - 1].y + 0.5f),
                            new Vector3(path[i].x + 0.5f, path[i].y + 0.5f)
                            );
                    }
                }
            }
        }
        
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
        
        public static void DrawRecipes() {
            foreach (Vector2Int v in ToolBox.cameraController.viewRect) {
                foreach (Recipe r in WorldUtils.recipes) {
                    if (r.finished) {
                        Gizmos.color = Color.green;
                    } else {
                        Gizmos.color = Color.blue;
                    }
                    Gizmos.DrawWireCube(
                        new Vector3(r.position.x+.5f, r.position.y+.5f), 
                        Vector3.one
                    );
                }
            }
        }
        
        public static void DrawAStar()
        {
            Color color1 = new Color(1, 0, 0, 0.5f);
            Color color2 = new Color(0, 1, 0, 0.5f);
            foreach (Vector2Int v in ToolBox.cameraController.viewRect)
            {
                Gizmos.color = Color.Lerp(color1, color2, ToolBox.map[v].pathCost);
                
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
                                
                    Gizmos.color = new Color(0f, 0.91f, 1f, 0.5f);
                                
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
        
        public static void DrawReserved()
        {
            Gizmos.color = new Color(0.97f, 0f, 0f);
            foreach (Vector2Int v in ToolBox.cameraController.viewRect)
            {
                if (ToolBox.map[v] != null && ToolBox.map[v].reserved)
                {
                    Gizmos.DrawWireCube(
                        new Vector3(v.x + 0.5f, v.y + 0.5f), 
                        Vector3.one
                    );
                }
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
