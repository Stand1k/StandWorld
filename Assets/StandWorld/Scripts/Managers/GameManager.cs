using System;
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
        public Map map;
        public bool DrawGizmosTiles;
        public bool DrawGizmosRegions;
        
        private bool _ready;
        
        private void Awake()
        {
            _ready = false;
            Res.Load();
            Defs.LoadGroundsFromCode();
        }

        private void Start()
        {
            map = new Map(100, 100);
            map.TempEverythingDirt();
            Debug.Log(map);
            foreach (MapRegion mapRegion in map.regions)
            {
                mapRegion.BuildMeshes();
            }
            
            _ready = true;
        }

        private void Update()
        {
            if (_ready)
            {
                foreach (MapRegion mapRegion in map.regions)
                {
                    mapRegion.Draw();
                    break;
                }
            }
        }

        private void OnDrawGizmos()
        {
            if (_ready)
            {
                if (DrawGizmosTiles)
                {
                    foreach (Tile t in map)
                    {
                        Ground g = (Ground) t.GetTilable(Layer.Ground);
                        if (g != null)
                        {
                            Gizmos.DrawWireCube(
                                new Vector3(g.position.x + .5f, g.position.y + .5f),
                                Vector3.one
                            );
                        }
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

