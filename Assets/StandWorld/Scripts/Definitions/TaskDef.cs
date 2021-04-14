using StandWorld.Characters.AI;

namespace StandWorld.Definitions
{
    [System.Serializable]
    public class TaskDef : Def
    {
        public TargetType targetType = TargetType.Tile;
        public TaskType taskType;
        public int ticksToPerform = 100;
    }
}