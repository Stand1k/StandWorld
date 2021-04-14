using System.Collections.Generic;
using StandWorld.Characters.AI;

namespace StandWorld.Definitions
{
    public static partial class Defs 
    {
        public static void AddTask(TaskDef def) 
        {
            tasks.Add(def.uId, def);
        }

        public static void LoadTasksFromCode()
        {
            tasks = new Dictionary<string, TaskDef>();
            
            AddTask(new TaskDef
            {
                uId = "task_cut",
                taskType = TaskType.Cut,
                targetType = TargetType.Adjacent,
            });
            
            AddTask(new TaskDef
            {
                uId = "task_harvers",
                taskType = TaskType.Harvest,
                targetType = TargetType.Adjacent,
            });
            
            AddTask(new TaskDef
            {
                uId = "task_sow",
                taskType = TaskType.Sow,
                targetType = TargetType.Adjacent,
            });
            
            AddTask(new TaskDef
            {
                uId = "task_dirt",
                taskType = TaskType.Dirt,
                targetType = TargetType.Adjacent,
            });

            AddTask(new TaskDef
            {
                uId = "task_sleep",
                taskType = TaskType.Sleep
            });

            AddTask(new TaskDef
            {
                uId = "task_idle",
                taskType = TaskType.Idle
            });
            
            AddTask(new TaskDef
            {
                uId = "task_eat",
                taskType = TaskType.Eat
            });
        }
    }
}