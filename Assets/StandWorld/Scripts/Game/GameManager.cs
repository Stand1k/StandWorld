using System.Collections;
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
        public bool DrawReserved;
        public bool DrawRecipes;

        [Header("World size")] public Vector2 mapSize;

        public bool ready => _ready;

        private bool _ready;

        private void Awake()
        {
            _ready = false;
            cameraController = GetComponentInChildren<CameraController>();
            stackableLabelController = GetComponentInChildren<StackableLabelController>();
            ToolBox.LoadStatics();
            ToolBox.NewGame(this);
        }

        private void Start()
        {
            tick = new Tick();
            map = new Map((int) mapSize.x, (int) mapSize.y);

            Debug.Log(map);
            map.TempMapGen();
            map.BuildAllMeshes();

            TestInit();

            StartCoroutine(TickUpdate());
            _ready = true;
        }

        public void TestInit()
        {
            int y = 22;
            for (int x = 10; x < 17; x++) {
                ToolBox.map.Spawn(new Vector2Int(x, y), new Building(
                    new Vector2Int(x, y),
                    Defs.buildings["wood_wall"]
                ));
            }
            y = 22;
            for (int x = 10; y < 26; y++) {
                ToolBox.map.Spawn(new Vector2Int(x, y), new Building(
                    new Vector2Int(x, y),
                    Defs.buildings["wood_wall"]
                ));
            }

            y = 23;
            for (int x = 16; y < 26; y++) {
                ToolBox.map.Spawn(new Vector2Int(x, y), new Building(
                    new Vector2Int(x, y),
                    Defs.buildings["wood_wall"]
                ));
            }
            
            y = 26;
            for (int x = 10; x < 17; x++) {
                ToolBox.map.Spawn(new Vector2Int(x, y), new Building(
                    new Vector2Int(x, y),
                    Defs.buildings["wood_wall"]
                ));
            }
            
            ToolBox.map.UpdateConnectedBuildings();
            
            
            StockArea stockarea = new StockArea(Defs.empty);
            stockarea.Add(new RectI(new Vector2Int(5, 5), 6, 6));

            GrowArea area = new GrowArea(Defs.plants["carrot"]);
            area.Add(new RectI(new Vector2Int(15, 15), 5, 5));

            /*for (int i = 0; i < 5; i++)
            {
                map.SpawnCharacter(new Animal(new Vector2Int(15, 15), Defs.animals["chiken"], new CharacterStats()));
            }*/

            for (int i = 0; i < 5; i++)
            {
                map.SpawnCharacter(new Human(new Vector2Int(15, 15), Defs.animals["human"], new HumanStats()));
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