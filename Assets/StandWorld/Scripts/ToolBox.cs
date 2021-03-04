using StandWorld.Controllers;
using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.Helpers;
using StandWorld.World;

namespace StandWorld
{
    public static class ToolBox
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
        
        
        public static Tick tick { get { return manager.tick; } }

        public static void LoadStatics()
        {
            Res.Load();
            DirectionUtils.SetNeighbours();
            Defs.LoadGroundsFromCode();
            Defs.LoadPlantsFromCode();
            Defs.LoadColorPalettesFromCode();
        }

        public static void NewGame(GameManager manager)
        {
            ToolBox.manager = manager;
        }
    }
}
