using System;
using StandWorld.Controllers;
using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.Helpers;
using StandWorld.Visuals;
using StandWorld.World;
using UnityEngine;

namespace StandWorld
{
    public class GameManager : MonoBehaviour
    {
        public CameraController cameraController;
        public Map map;
        public bool DrawGizmosTiles;
        public bool DrawGizmosRegions;
        public bool DrawNoiseMap;
        public bool DrawBuckets;
        
        private bool _ready;
        
        private void Awake()
        {
            _ready = false;
            cameraController = FindObjectOfType<CameraController>();
            ToolBox.LoadStatics();
            ToolBox.NewGame(this);
        }

        private void Start()
        {
            map = new Map(150, 150);
            Debug.Log(map);
            map.TempMapGen();
            map.groundGrid.BuildStaticMeshes();
            //map.BuildAllRegionMeshes();
            
            _ready = true;
        }

        private void Update()
        {
            if (_ready)
            {
               // map.DrawRegions();
               map.groundGrid.DrawBuckets();
            }
        }

        private void OnDrawGizmos()
        {
            if (_ready)
            {
                if (DrawBuckets)
                {
                    foreach (Vector2Int v in map.mapRect)
                    {
                        LayerGridBucket bucket = map.groundGrid.GetBucketAt(v);
                        Gizmos.color = new Color(bucket.uId / (float)map.groundGrid.buckets.Length, 0f, 0, 0.6f);
                        Gizmos.DrawCube(
                            new Vector3(v.x + .5f, v.y + .5f),
                            Vector3.one
                        );
                    }
                }
                
                if (DrawNoiseMap)
                {
                    foreach (Vector2Int v in cameraController.viewRect)
                    {
                        float h = map.groundNoiseMap[v.x + v.y * map.size.x];
                        Gizmos.color = new Color(h, h, h, 1f);
                        Gizmos.DrawCube(
                            new Vector3(v.x + .5f, v.y + .5f),
                            Vector3.one
                        );
                    }
                }
                
                if (DrawGizmosTiles)
                {
                    foreach (Vector2Int v in cameraController.viewRect)
                    {
                        Gizmos.DrawWireCube(
                            new Vector3(v.x+.5f, v.y+.5f), 
                            Vector3.one
                        );
                    }
                }

                if (DrawGizmosRegions)
                {
                    foreach (MapRegion region in map.regions)
                    {
                        Gizmos.color = new Color(0, 0, 1, 0.5f);
                        Gizmos.DrawCube(
                                new Vector3(
                                    region.regionRect.max.x - (region.regionRect.width - 0.5f) / 2,
                                    region.regionRect.max.y - (region.regionRect.height - 0.5f) / 2 
                                    ),
                            new Vector3(region.regionRect.width - 0.5f, region.regionRect.height - 0.5f)
                            );
                    }
                }
            }
        }
            
    }
}

