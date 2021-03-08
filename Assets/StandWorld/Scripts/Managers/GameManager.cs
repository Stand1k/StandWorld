using System.Collections;
using StandWorld.Controllers;
using StandWorld.Entities;
using StandWorld.World;
using UnityEngine;

namespace StandWorld
{
    public class GameManager : MonoBehaviour
    {
        public CameraController cameraController;
        public Map map;
        public Tick tick;
        
        public bool DrawGizmosTiles;
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
            tick = new Tick();
            map = new Map(300, 300);
            
            Debug.Log(map);
            map.TempMapGen();
            map.groundGrid.BuildStaticMeshes();

            StartCoroutine(TickUpdate());
            _ready = true;
        }

        private void Update()
        {
            if (_ready)
            {
               map.groundGrid.DrawBuckets();
               map.plantGrid.DrawBuckets(); 
            }
        }

        IEnumerator TickUpdate()
        {
            for (;;)
            {
                yield return new WaitForSeconds(0.1f / tick.speed);
                tick.DoTick();
            }
        }
        
        private void OnDrawGizmos()
        {
            if (_ready)
            {
                if (DrawBuckets)
                {
                    for (int x = 0; x <  map.size.x; x += Settings.BUCKET_SIZE)
                    {
                        for (int y = 0; y < map.size.y; y += Settings.BUCKET_SIZE)
                        {
                            LayerBucketGrid bucket = map.groundGrid.GetBucketAt(new Vector2Int(x,y));
                            
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
                
                if (DrawNoiseMap)
                {
                    foreach (Vector2Int v in map.mapRect)
                    {
                        float h = map.groundNoiseMap[v.x + v.y * map.size.x];
                        Gizmos.color = new Color(h, h, h, 1f);
                        Gizmos.DrawCube(
                            new Vector3(v.x + 0.5f, v.y + 0.5f),
                            Vector3.one
                        );
                    }
                }
                
                if (DrawGizmosTiles)
                {
                    foreach (Vector2Int v in cameraController.viewRect)
                    {
                        Gizmos.DrawWireCube(
                            new Vector3(v.x + 0.5f, v.y + 0.5f), 
                            Vector3.one
                        );
                    }
                }
                
            }
        }
            
    }
}

