using System.Collections;
using StandWorld.Characters;
using StandWorld.Controllers;
using StandWorld.Entities;
using StandWorld.Visuals;
using StandWorld.World;
using UnityEngine;
    
namespace StandWorld.Game
{
    public class GameManager : MonoBehaviour
    {
        public CameraController cameraController;
        public StackableLabelController stackableLabelController;
        public Map map;
        public Tick tick;
        
        public bool DrawGizmosTiles;
        public bool DrawNoiseMap;
        public bool DrawBuckets;

        [Header("World size")] 
        public Vector2 mapSize;
        [Space]
        
        private bool _ready;

        private void Awake()
        {
            _ready = false;
            cameraController = FindObjectOfType<CameraController>();
            stackableLabelController = GetComponentInChildren<StackableLabelController>();
            ToolBox.LoadStatics();
            ToolBox.NewGame(this);
        }

        private void Start()
        {
            tick = new Tick();
            map = new Map((int)mapSize.x, (int)mapSize.y);
            
            Debug.Log(map);
            map.TempMapGen();
            map.BuildAllMeshes();
            
            Debug.Log(new HumanStats());
            Debug.Log(new HumanStats());
            Debug.Log(new HumanStats());
            Debug.Log(new HumanStats());
            Debug.Log(new HumanStats());
         
            StartCoroutine(TickUpdate());
            _ready = true;
        }

        private void Update()
        {
            if (_ready)
            {
             map.DrawTilables();
            }
        }

        private void LateUpdate()
        {
            if (_ready)
            {
                map.CheckAllMatrices();
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
            if (_ready && Settings.DEBUG)
            {
                if (DrawBuckets)
                {
                   DebugRenderer.DrawBuckets();
                }
                
                if (DrawNoiseMap)
                {
                    DebugRenderer.DrawNoiseMap();
                }
                
                if (DrawGizmosTiles)
                {
                    DebugRenderer.DrawTiles();
                }
                
            }
        }
            
    }
}

