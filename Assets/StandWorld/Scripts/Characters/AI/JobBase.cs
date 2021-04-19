using System.Collections.Generic;

namespace StandWorld.Characters.AI
{
    public abstract class JobBase : TaskBase
    {
        public Queue<Job> jobs = new Queue<Job>();
        public Job job { get; protected set; }

        public JobBase(BaseCharacter character, Task task) : base(character, task)
        {
            if (OnStart != null)
            {
                OnStart();
            }
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
                OnEnd = job.OnEnd;
                OnStart = job.OnStart;
                OnTick = job.OnTick;
                _inRange = false;

                if (task.def.targetType == TargetType.Adjacent)
                {
                    bool path = task.targets.current.ClosestNeighbour(character.position);
                    if (!path)
                    {
                        task.state = TaskState.Failed;
                    }
                }
                else if (task.def.targetType == TargetType.None)
                {
                    _inRange = true;
                }
            }
            else
            {
                if (job.interuptable)
                {
                    task.state = TaskState.Failed;
                    task.targets.FreeAll();
                }
                else
                {
                    if (jobs.Count == 0)
                    {
                        task.state = TaskState.Failed;
                        task.targets.FreeAll();
                    }
                    else
                    {
                        Next();
                    }
                }
            }
        }

        public override void Tick()
        {
            if (task.state != TaskState.Running)
            {
                return;
            }

            if (_inRange)
            {
                if (ticks == 0 && job.OnStart != null)
                {
                    job.OnStart();
                }

                if (Perform())
                {
                    End();
                }
            }
            else
            {
                if (task.def.targetType != TargetType.None)
                {
                    MoveInRange();
                }
            }
        }

        public override void End()
        {
            task.state = TaskState.Success;
            task.targets.Free();

            if (OnEnd != null)
            {
                OnEnd();
            }
        }

        public override bool Perform()
        {
            if (task.ticksToPerform == 0)
            {
                if (job.OnEnd != null)
                {
                    job.OnEnd();
                }

                if (jobs.Count == 0)
                {
                    return true;
                }

                Next();
            }
            else
            {
                ticks++;
                if (OnTick != null)
                {
                    OnTick();
                }

                if (job.OnTick != null)
                {
                    job.OnTick();
                }

                if (ticks >= task.ticksToPerform)
                {
                    if (job.OnEnd != null)
                    {
                        job.OnEnd();
                    }

                    if (jobs.Count == 0)
                    {
                        return true;
                    }

                    Next();
                }
            }

            return false;
        }
    }
}