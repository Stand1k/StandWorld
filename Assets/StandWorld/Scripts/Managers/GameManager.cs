using System;
using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.World;
using UnityEngine;

namespace StandWorld
{
    public class GameManager : MonoBehaviour
    {
        public Map map;
        private bool _ready;
        
        private void Awake()
        {
            this._ready = false;
            Defs.LoadGroundFromCode();
        }

        private void Start()
        {
            this.map = new Map(150, 150);
            this.map.TempEverythingDirt();
            Debug.Log(this.map);
            this._ready = true;
        }

        private void OnDrawGizmos()
        {
            if (this._ready)
            {
                foreach(Tile t in this.map)
                {
                    Ground g = (Ground)t.GetTilable(Layer.Ground);
                    if (g != null)
                    {
                        Gizmos.DrawCube(
                            new Vector3(g.position.x + .5f, g.position.y + .5f),
                            Vector3.one
                            );
                    }
                }
            }
        }
    }
}

