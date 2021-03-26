using StandWorld.Definitions;

namespace StandWorld.Characters.AI
{
    public class TaskRunner
    {
        public TaskDef def { get; protected set; }
        public Task task { get; protected set; } 
        public bool running { get; protected set; }

        public TaskRunner()
        {
            running = false;
        }

        public void StartTask(TaskDef def, BaseCharacter character, TargetList targets)
        {
            this.def = def;
            if (this.def.taskType == TaskType.Sleep)
            {
                task = new TaskSleep(character, this, targets);
            }
            else if (this.def.taskType == TaskType.Idle)
            {
                task = new TaskIdle(character, this, targets);
            }

            running = true;
        }

        public void EndTask()
        {
            running = false;
        }
    }
}