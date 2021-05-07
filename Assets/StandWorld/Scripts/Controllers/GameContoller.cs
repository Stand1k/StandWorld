using System.Collections;
using ProjectPorcupine.Localization;
using StandWorld.Characters;
using StandWorld.Controllers;
using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.Helpers;
using StandWorld.UI;
using StandWorld.Visuals;
using StandWorld.World;
using UnityEngine;

namespace StandWorld.Game
{
    public class GameContoller : MonoBehaviour
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
        public bool DrawReserved;
        public bool DrawRecipes;

        [Header("World size")] public Vector2Int mapSize;

        public bool ready => _ready;

        private bool _ready;

        private void Awake()
        {
            mapSize = Settings.mapSize;
            _ready = false;
            cameraController = GetComponentInChildren<CameraController>();
            stackableLabelController = GetComponentInChildren<StackableLabelController>();
            ToolBox.LoadStatics(); //TODO: MainMenu Load
            ToolBox.NewGame(this);
        }

        private void Start()
        {
            tick = new Tick();
            map = new Map(mapSize.x, mapSize.y);
            gameObject.AddComponent<LocalizationLoader>();

            Debug.Log(map);
            map.TempMapGen();
            map.BuildAllMeshes();

            TestInit();

            StartCoroutine(TickUpdate());
            _ready = true;
        }

        public void TestInit()
        {
            /*
            StockArea stockarea = new StockArea(Defs.empty);
            stockarea.Add(new RectI(new Vector2Int(5, 5), 6, 6));*/

            /*GrowArea area = new GrowArea(Defs.plants["carrot"]);
            area.Add(new RectI(new Vector2Int(135, 155), 5, 5));*/
            
            CharacterSpawner(4);
            AnimalSpawner(21);
        }

        public void CharacterSpawner(int count)
        {
            int randomRegion = Random.Range(0, mapSize.x / Settings.BUCKET_SIZE);
            Vector2Int randomPosition = Vector2Int.zero;
            
            for (int i = 0; i < count; i++)
            {
                bool isSpawn = false;
                int counter = 0;
                
                while (!isSpawn)
                {
                    var rect = map.grids[Layer.Ground].buckets[randomRegion].rect;
                    
                    randomPosition = new Vector2Int(
                        Random.Range(rect.min.x, rect.max.x),
                        Random.Range(rect.min.y, rect.max.y));

                    if (!map[randomPosition].blockPath)
                    {
                        map.SpawnCharacter(new Human(randomPosition, Defs.animals["human"], new HumanStats()));
                        isSpawn = true;
                    }

                    counter++;

                    if (counter >= rect.area)
                    {
                        if (randomRegion + 1 > mapSize.x / Settings.BUCKET_SIZE)
                        {
                            randomRegion--;
                        }
                        else
                        {
                            randomRegion++;
                        }
                        
                        break;
                    }
                }
            }

            Camera.main.gameObject.transform.position = new Vector3(randomPosition.x,randomPosition.y, -100); //TODO: LOL
        }
        
        public void AnimalSpawner(int count)
        {
            for (int i = 0; i < count; i++)
            {
                bool isSpawn = false;
                
                while (!isSpawn)
                {
                    Vector2Int randomPosition = new Vector2Int(Random.Range(1, mapSize.x - 1), Random.Range(1, mapSize.y - 1));

                    if (!map[randomPosition].blockPath)
                    {
                        map.SpawnCharacter(new Animal(randomPosition, Defs.animals["chiken"], new CharacterStats()));
                        isSpawn = true;
                    }
                }
            }

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
            if (_ready)
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

                if (DrawReserved)
                {
                    DebugRenderer.DrawReserved();
                }
                
                if (DrawRecipes)
                {
                    DebugRenderer.DrawRecipes();
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