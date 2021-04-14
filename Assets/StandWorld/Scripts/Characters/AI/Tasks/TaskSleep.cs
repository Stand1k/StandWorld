namespace StandWorld.Characters.AI
{
    public class TaskSleep : TaskBase
    {
        public TaskSleep(BaseCharacter character, Task task) : base(character, task)
        {
        }

        public override bool Perform()
        {
            if (character.stats.sleep == false)
            {
                character.stats.Sleep();
                return false;
            }

            if (character.stats.vitals[Vitals.Energy].currentValue < character.stats.vitals[Vitals.Energy].value)
            {
                return false;
            }

            character.stats.WakeUp();
            return true;
        }
    }
}