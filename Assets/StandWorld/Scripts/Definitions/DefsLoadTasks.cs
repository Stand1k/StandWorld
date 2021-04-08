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
                uID = "task_cut",
                taskType = TaskType.Cut,
                targetType = TargetType.Adjacent,
            });
            
            AddTask(new TaskDef
            {
                uID = "task_harvers",
                taskType = TaskType.Harvers,
                targetType = TargetType.Adjacent,
            });
            
            AddTask(new TaskDef
            {
                uID = "task_sow",
                taskType = TaskType.Sow,
                targetType = TargetType.Adjacent,
            });
            
            AddTask(new TaskDef
            {
                uID = "task_dirt",
                taskType = TaskType.Dirt,
                targetType = TargetType.Adjacent,
            });

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
            
            AddTask(new TaskDef
            {
                uID = "task_eat",
                taskType = TaskType.Eat
            });
        }
    }
}