using System.Collections.Generic;
using StandWorld.Characters.AI;

namespace StandWorld.Definitions
{
    public static partial class Defs 
    {
        public static void AddTask(TaskDef def) 
        {
            tasks.Add(def.uID, def);
        }

        public static void LoadTasksFromCode()
        {
            tasks = new Dictionary<string, TaskDef>();

            AddTask(new TaskDef
            {
                uID = "task_sleep",
                taskType = TaskType.Sleep
            });

            AddTask(new TaskDef
            {
                uID = "task_idle",
                taskType = TaskType.Idle
            });
        }
    }
}