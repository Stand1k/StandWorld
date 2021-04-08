using StandWorld.Entities;
using StandWorld.Game;

namespace StandWorld.Characters.AI
{
    public class TaskCut : Task
    {
        public TaskCut(TaskData taskData, TaskRunner taskRunner) : base(taskData, taskRunner)
        {
        }

        public override bool Perform()
        {
            Plant plant = (Plant) targets.current.entity;
            
            plant.Cut();
            return true;
        }
    }
    
    public class TaskDirt: Task
    {
        public TaskDirt(TaskData taskData, TaskRunner taskRunner) : base(taskData, taskRunner)
        {
        }

        public override bool Perform()
        {
            Field field = (Field) targets.current.entity;
            
            field.WorkDirt();
            return true;
        }
    }
    
    public class TaskSow: Task
    {
        public TaskSow(TaskData taskData, TaskRunner taskRunner) : base(taskData, taskRunner)
        {
        }

        public override bool Perform()
        {
            Field field = (Field) targets.current.entity;
            
            ToolBox.map.Spawn(targets.current.position, new Plant(
                field.position,
                field.growArea.def
                ));
            return true;
        }
    }
}