using StandWorld.Controllers;
using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.Helpers;
using StandWorld.World;

namespace StandWorld.Game
{
    public static class ToolBox
    {
        public static GameContoller contoller;
        public static CameraController cameraController => contoller.cameraController;

        public static Map map => contoller.map;

        public static StackableLabelController stackableLabelController => contoller.stackableLabelController;

        public static Tick tick => contoller.tick;

        public static void LoadStatics()
        {
            Res.Load();
            DirectionUtils.SetNeighbours();
            Defs.LoadDefaultDefs();
            Defs.LoadGroundsFromCode();
            Defs.LoadPlantsFromCode();
            Defs.LoadMountainsFromCode();    
            Defs.LoadStackablesFromCode();    
            Defs.LoadColorPalettesFromCode();
            Defs.LoadAnimalsFromCode();
            Defs.LoadTasksFromCode();
            Defs.LoadMenuOrdersFromCode();
            Defs.LoadBuildingsFromCode();
        }

        public static void NewGame(GameContoller contoller)
        {
            ToolBox.contoller = contoller;
        }
    }
}
