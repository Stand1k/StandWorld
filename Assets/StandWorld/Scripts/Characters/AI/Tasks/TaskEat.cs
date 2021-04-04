using StandWorld.Entities;

namespace StandWorld.Characters.AI
{
    public class TaskEat : Task
    {
        public TaskEat(TaskData taskData, TaskRunner taskRunner) : base(taskData, taskRunner)
        {
        }

        public override bool Perform()
        {
            Tilable tilable = (Tilable) targets.current.entity;
            character.stats.vitals[Vitals.Hunger].currentValue += tilable.def.nutriments * 100f;
         
            tilable.Destroy();
            return true;
        }
    }
}