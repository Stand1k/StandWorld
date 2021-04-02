using System;
using StandWorld.Definitions;

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

            running = true;
        }

        public void EndTask()
        {
            if (onEndTask != null)
            {
                onEndTask();
            }
            
            running = false;
            task = null;
        }
    }
}