using System;
using StandWorld.Definitions;

namespace StandWorld.Characters.AI
{
    [Serializable]
    public class TaskData
    {
        public TaskDef def;
        public TargetList targets;
        public BaseCharacter character;
        public int ticksToPerform;
        
        public TaskData(TaskDef def, TargetList targets, BaseCharacter character, int ticksToPerform = 0)
        {
            this.def = def;
            this.targets = targets;
            this.character = character;
            this.ticksToPerform = ticksToPerform;
        }
    }
}