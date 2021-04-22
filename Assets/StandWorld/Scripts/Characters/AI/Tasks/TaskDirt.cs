using StandWorld.Entities;

namespace StandWorld.Characters.AI
{
    public class TaskDirt: TaskBase
    {
        public TaskDirt(BaseCharacter character, Task task) : base(character, task)
        {
        }

        public override bool Perform()
        {
            GardenField gardenField = (GardenField) task.targets.current.tilable;
            
            gardenField.WorkDirt();
            return true;
        }
    }
}