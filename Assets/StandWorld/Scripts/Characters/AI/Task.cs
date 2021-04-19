using StandWorld.Characters.AI.Jobs;
using StandWorld.Definitions;

namespace StandWorld.Characters.AI
{
    public enum TaskState
    {
        Running,
        Success,
        Failed,
    }

    public enum TaskType
    {
        Idle,
        Sleep,
        Eat,
        Cut,
        Harvest,
        Sow,
        Dirt,
        HaulRecipe,
    }

    public class Task
    {
        public TargetList targets;
        public TaskState state;
        public TaskDef def;
        public TaskBase taskBase;
        public int ticksToPerform;

        public Task(TaskDef def)
        {
            this.def = def;
            ticksToPerform = def.ticksToPerform;
        }

        public Task(TaskDef def, TargetList targets) : this(def)
        {
            this.targets = targets;
            ticksToPerform = def.ticksToPerform;
        }

        public Task(TaskDef def, TargetList targets, int ticksToPerform) : this(def, targets)
        {
            this.ticksToPerform = ticksToPerform;
        }

        public Task(TaskDef def, int ticksToPerform) : this(def)
        {
            this.ticksToPerform = ticksToPerform;
        }

        public void GetTaskClass(BaseCharacter character)
        {
            switch (def.taskType)
            {
                case TaskType.Cut:
                    taskBase = new TaskCut(character, this);
                    break;
                case TaskType.Dirt:
                    taskBase = new TaskDirt(character, this);
                    break;
                case TaskType.Sow:
                    taskBase = new TaskSow(character, this);
                    break;
                case TaskType.Eat:
                    taskBase = new TaskEat(character, this);
                    break;
                case TaskType.Sleep:
                    taskBase = new TaskSleep(character, this);
                    break;
                case TaskType.Idle:
                    taskBase = new TaskIdle(character, this);
                    break;
                case TaskType.HaulRecipe:
                    taskBase = new HaulRecipeJob(character, this);
                    break;
            }
        }
    }
}