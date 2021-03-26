namespace StandWorld.Characters.AI
{
    public class TaskIdle : Task
    {
        public TaskIdle(BaseCharacter character, TaskRunner taskRunner, TargetList targets) : base(character, taskRunner, targets)
        {
        }
    }
}