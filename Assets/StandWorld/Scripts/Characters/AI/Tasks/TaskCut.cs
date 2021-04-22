using StandWorld.Entities;

namespace StandWorld.Characters.AI
{
    public class TaskCut : TaskBase
    {
        public TaskCut(BaseCharacter character, Task task) : base(character, task)
        {
        }

        public override bool Perform()
        {
            Plant plant = (Plant) task.targets.current.tilable;
            
            plant.Cut();
            return true;
        }
    }
}