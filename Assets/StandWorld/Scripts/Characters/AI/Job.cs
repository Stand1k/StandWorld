using System;

namespace StandWorld.Characters.AI
{
    public class Job
    {
        public JobBase taskBase;
        public bool interuptable = false;

        public Func<bool> preCondition;
        public Action OnEnd = null;
        public Action OnTick = null;
        public Action OnStart = null;

        public Job(Func<bool> preCondition)
        {
            this.preCondition = preCondition;
        }

        public Job()
        {
            preCondition = null;
        }
    }
}