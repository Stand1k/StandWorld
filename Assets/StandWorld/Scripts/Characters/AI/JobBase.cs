using System.Collections.Generic;

namespace StandWorld.Characters.AI
{
    public class JobBase : TaskBase
    {
        public Queue<Job> jobs = new Queue<Job>();
        public Job job { get; protected set; }

        public JobBase(BaseCharacter character, Task task) : base(character, task)
        {
        }

        public void Next(bool next = true)
        {
            if (jobs.Count == 0)
            {
                task.state = TaskState.Success;
                return;
            }

            job = jobs.Dequeue();
            if (next)
            {
                task.targets.Next();
            }

            if (job.preCondition == null || job.preCondition())
            {
                ticks = 0;
                task.state = TaskState.Running;
                OnEnd += OnEnd;
                OnStart += OnStart;
                OnTick += OnTick;

                if (task.def.targetType == TargetType.Adjacent)
                {
                    bool path = task.targets.current.ClosestNeighbour(character.position);
                    if (!path)
                    {
                        task.state = TaskState.Failed;
                    }
                }
            }
            else
            {
                if (job.interuptable)
                {
                    task.state = TaskState.Failed;
                }
                else
                {
                    if (jobs.Count == 0)
                    {
                        task.state = TaskState.Failed; 
                    }
                    else
                    {
                        Next();
                    }
                }
            }
        }

        public override void End()
        {
            task.state = TaskState.Success;
            task.targets.Free();
        }

        public override bool Perform()
        {
            ticks++;
            if (OnTick != null)
            {
                OnTick();
            }

            if (ticks >= task.ticksToPerform)
            {
                if (jobs.Count == 0)
                {
                    return true;
                }

                Next();
            }

            return false;
        }
    }
}