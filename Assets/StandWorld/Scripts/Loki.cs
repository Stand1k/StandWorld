using StandWorld.Controllers;
using StandWorld.Definitions;
using StandWorld.Helpers;
using StandWorld.World;
using UnityEngine;

namespace StandWorld
{
    public class Loki
    {
        public static GameManager manager;
        public static CameraController cameraController
        {
            get { return manager.cameraController; }
        }
        public static Map map
        {
            get { return manager.map; }
        }

        public static void LoadStatics()
        {
            Res.Load();
            Defs.LoadGroundsFromCode();
            Defs.LoadPlantsFromCode();
            Defs.LoadColorPalettesFromCode();
        }

        public static void NewGame(GameManager manager)
        {
            Loki.manager = manager;
        }
    }
}
