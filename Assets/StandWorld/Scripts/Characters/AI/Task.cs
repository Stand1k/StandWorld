using System;
using StandWorld.Definitions;
using StandWorld.Entities;

namespace StandWorld.Characters.AI
{
    [Serializable]
    public enum TargetType
    {
        None,
        Tile,
        Adjacent,
    }

    [Serializable]
    public enum TaskStatus
    {
        Running,
        Success,
        Failed,
    }

    [Serializable]
    public enum TaskType
    {
        Idle,
        Sleep,
        Eat,
    }

    public abstract class Task
    {
        public BaseCharacter character { get; protected set; }
        public TaskRunner taskRunner { get; protected set; }
        public TaskStatus taskStatus { get; set; }
        public TargetList targets { get; protected set; }
        public TaskDef def => taskRunner.def;

        private bool _start = false;
        private bool _inRange = false;
        private int _ticks = 0;
        private int _tickToPerform = 0;

        public Task(BaseCharacter character, TaskRunner taskRunner, TargetList targets)
        {
            this.character = character;
            this.taskRunner = taskRunner;
            this.targets = targets;
            Start();
        }

        public virtual void Update()
        {
            if (taskStatus != TaskStatus.Running)
            {
                taskRunner.EndTask();
                return;
            }

            if (def.targetType == TargetType.None)
            {
               Run();
            }
            else
            {
                if (!_inRange)
                {
                    MoveInRange();
                }
                else
                {
                    Run();
                }
            }
        }

        private void MoveInRange()
        {
            if (targets.current == null)
            {
                taskStatus = TaskStatus.Failed;
                return;
            }

            if (character.position == targets.currentPosition)
            {
                _inRange = true;
                return;
            }
                
            character.movement.Move(this);
        }

        private void Run()
        {
            if (!_start)
            {
                _start = true;
                Start();
            }

            if (Perform())
            {
                End();
            }
        }
        
        public virtual void End()
        {
            taskStatus = TaskStatus.Success;
        }
        
        public virtual void Start()
        {
            taskStatus = TaskStatus.Running;
            _tickToPerform = def.ticksToPerform;
        }
        
        public virtual bool Perform()
        {
            if (def.ticksToPerform == 0)
            {
                End();
                return true;
            }

            _ticks++;
            if (_ticks >= _tickToPerform)
            {
                End();
                return true;
            }

            return false;
        }

    }
}