using TMPro;

namespace StandWorld.Definitions
{
    public class BuildingDef : TilableDef
    {
        public int work = 100;
        public new bool blockPath = true;
        public new bool blockStackable = true;
        public new bool supportRoof = true;
        public new bool blockBuilding = true;
        public new bool blockPlant = true;
    }
}