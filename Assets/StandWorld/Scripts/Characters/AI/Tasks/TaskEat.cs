using StandWorld.Entities;

namespace StandWorld.Characters.AI
{
    public class TaskEat : TaskBase
    {
        public TaskEat(BaseCharacter character, Task task) : base(character, task)
        {
        }

        public override bool Perform()
        {
            Tilable tilable = task.targets.current.tilable;
            character.stats.vitals[Vitals.Hunger].currentValue += tilable.tilableDef.nutriments * 100f;
         
            tilable.Destroy();
            return true;
        }
    }
}