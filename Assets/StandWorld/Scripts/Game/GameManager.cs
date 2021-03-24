using System.Collections;
using StandWorld.Characters;
using StandWorld.Characters.AI;
using StandWorld.Controllers;
using StandWorld.Definitions;
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
        public bool DrawFertility;
        public bool DrawAStar;
        public bool DrawPaths;

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
            
            map.SpawnCharacter(new Animal(new Vector2Int(10, 10), Defs.animals["chiken"]));
            map.SpawnCharacter(new Animal(new Vector2Int(10, 10), Defs.animals["chiken"]));
            map.SpawnCharacter(new Animal(new Vector2Int(10, 10), Defs.animals["chiken"]));
            
         
            StartCoroutine(TickUpdate());
            _ready = true;
        }

        private void Update()
        {
            if (_ready)
            {
                map.DrawTilables();
                map.UpdateCharacters();
            }
        }

        private void LateUpdate()
        {
            if (_ready)
            {
                map.CheckAllMatrices();
            }
        }


        private float time = 5f;

        void TimeMinuas(float decreaser)
        {
            if (time < 0f) 
            {
                tick.DoTick();
                time = 5f;
            }

            time -= Time.fixedTime;
        }
        
        IEnumerator TickUpdate()
        {
            for (;;)
            {
                yield return new WaitForSeconds(0.01f / tick.speed);
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

                if (DrawFertility)
                {
                    DebugRenderer.DrawFertility();
                }

                if (DrawAStar)
                {
                    DebugRenderer.DrawAStar();
                }

                if (DrawPaths)
                {
                    foreach (BaseCharacter character in map.characters)
                    {
                        DebugRenderer.DrawCurrentPath(character.movement);
                    }
                }

            }
        }
            
    }
}

