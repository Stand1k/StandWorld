using StandWorld.Controllers;
using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.Helpers;
using StandWorld.World;

namespace StandWorld.Game
{
    public static class ToolBox
    {
        public static GameManager manager;
        public static CameraController cameraController => manager.cameraController;

        public static Map map => manager.map;

        public static StackableLabelController stackableLabelController => manager.stackableLabelController;


        public static Tick tick { get { return manager.tick; } }

        public static void LoadStatics()
        {
            Res.Load();
            DirectionUtils.SetNeighbours();
            Defs.LoadGroundsFromCode();
            Defs.LoadPlantsFromCode();
            Defs.LoadMountainsFromCode();    
            Defs.LoadStackablesFromCode();    
            Defs.LoadColorPalettesFromCode();
            Defs.LoadAnimalsFromCode();
        }

        public static void NewGame(GameManager manager)
        {
            ToolBox.manager = manager;
        }
    }
}
