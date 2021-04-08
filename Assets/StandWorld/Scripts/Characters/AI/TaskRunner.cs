using System;
using StandWorld.Definitions;
using StandWorld.Game;

namespace StandWorld.Characters.AI
{
    public class TaskRunner
    {
        public TaskDef def { get; protected set; }
        public Task task { get; protected set; }
        public bool running { get; protected set; }
        public Action onEndTask = null;

        public TaskRunner()
        {
            running = false;
        }

        public void StartTask(TaskData taskData)
        {
            def = taskData.def;
            
            if (def.taskType == TaskType.Sleep)
            {
                task = new TaskSleep(taskData, this);
            }
            else if (def.taskType == TaskType.Idle)
            {
                task = new TaskIdle(taskData, this);
            }
            else if (def.taskType == TaskType.Eat)
            {
                task = new TaskEat(taskData, this);
            }
            else if (def.taskType == TaskType.Cut)
            {
                task = new TaskCut(taskData, this);
            }
            else if (def.taskType == TaskType.Dirt)
            {
                task = new TaskDirt(taskData, this);
            }
            else if (def.taskType == TaskType.Sow)
            {
                task = new TaskSow(taskData, this);
            }
            
            running = true;
        }

        public void EndTask()
        {
            if (onEndTask != null)
            {
                onEndTask();
            }

            if (task.targets.current != null)
            {
                ToolBox.map[task.targets.current.position].reserved = false;
            }

            running = false;
            task = null;
        }
    }
}