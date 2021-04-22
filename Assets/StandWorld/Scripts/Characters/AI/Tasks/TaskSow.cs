using StandWorld.Entities;
using StandWorld.Game;

namespace StandWorld.Characters.AI
{
    public class TaskSow: TaskBase
    {
        public TaskSow(BaseCharacter character, Task task) : base(character, task)
        {
        }

        public override bool Perform()
        {
            GardenField gardenField = (GardenField) task.targets.current.tilable;
            
            ToolBox.map.Spawn(task.targets.current.position, new Plant(
                gardenField.position,
                gardenField.growArea.def
            ));
            return true;
        }
    }
}