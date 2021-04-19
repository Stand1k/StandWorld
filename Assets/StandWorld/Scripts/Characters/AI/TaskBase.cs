using System;

namespace StandWorld.Characters.AI
{
    public abstract class TaskBase
    {
        public BaseCharacter character;

        public Task task;
        public int ticks;
        public Action OnEnd = null;
        public Action OnStart = null;
        public Action OnTick = null;
        protected bool _inRange;

        public TaskBase(BaseCharacter character, Task task)
        {
            this.character = character;
            this.task = task;
            this.task.state = TaskState.Running;

            if (this.task.def.targetType == TargetType.Adjacent)
            {
                bool path = this.task.targets.current.ClosestNeighbour(this.character.position);
                if (!path)
                {
                    this.task.state = TaskState.Failed;
                }
            }
            else if (this.task.def.targetType == TargetType.None)
            {
                _inRange = true;
            }
        }

        public virtual void Tick()
        {
            if (task.state != TaskState.Running)
            {
                return;
            }

            if (_inRange)
            {
                if (ticks == 0 && OnStart != null)
                {
                    OnStart();
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

        public virtual void End()
        {
            task.state = TaskState.Success;
            task.targets.Free();
            
            if (OnEnd != null)
            {
                OnEnd();
            }
        }

        public virtual bool Perform()
        {
            ticks++;
            if (OnTick != null)
            {
                OnTick();
            }

            if (ticks >= task.ticksToPerform)
            {
                return true;
            }

            return false;
        }

        protected void MoveInRange()
        {
            if (task.targets.current == null)
            {
                task.state = TaskState.Failed;  
            }
            else
            {
                if (character.position == task.targets.currentPosition)
                {
                    _inRange = true;
                    return;
                }

                character.movement.Move(task);
            }
        }
    }
}