using System;
using StandWorld.Controllers;
using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.Helpers;
using StandWorld.World;
using UnityEngine;

namespace StandWorld.Game
{
    public class ToolBox : Singleton<ToolBox>, IDisposable
    {
        public GameContoller contoller;
        public CameraController cameraController => contoller.cameraController;
        public readonly int Speed = Shader.PropertyToID("_Speed");
        public readonly int ScrollSpeed = Shader.PropertyToID("_ScrollSpeed");

        public Map map
        {
            get => contoller.map;
            set => contoller.map = value;
        }

        public StackableLabelController stackableLabelController => contoller.stackableLabelController;

        public Tick tick
        {
            get => contoller.tick;
            set => contoller.tick = value;
        }

        public void LoadStatics()
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

        public void NewGame(GameContoller contoller)
        {
            Instance.contoller = contoller; 
        }

        public void Dispose()
        {
            //WorldUtils.recipes = null;
            Destroy(contoller);
            Destroy(cameraController);
            Destroy(stackableLabelController);
            map = null;
            tick = null;
        }
    }
}
