using StandWorld.Entities;
using StandWorld.Game;

namespace StandWorld.Characters.AI
{
    public class TaskCut : TaskBase
    {
        public TaskCut(BaseCharacter character, Task task) : base(character, task)
        {
        }

        public override bool Perform()
        {
            Plant plant = (Plant) task.targets.current.entity;
            
            plant.Cut();
            return true;
        }
    }
    
    public class TaskDirt: TaskBase
    {
        public TaskDirt(BaseCharacter character, Task task) : base(character, task)
        {
        }

        public override bool Perform()
        {
            Field field = (Field) task.targets.current.entity;
            
            field.WorkDirt();
            return true;
        }
    }
    
    public class TaskSow: TaskBase
    {
        public TaskSow(BaseCharacter character, Task task) : base(character, task)
        {
        }

        public override bool Perform()
        {
            Field field = (Field) task.targets.current.entity;
            
            ToolBox.map.Spawn(task.targets.current.position, new Plant(
                field.position,
                field.growArea.def
                ));
            return true;
        }
    }
}