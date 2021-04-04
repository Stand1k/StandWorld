using System;
using StandWorld.Definitions;
using UnityEngine;

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

        private bool _inRange;
        private int _ticks;
        private int _ticksToPerform;

        public Task(TaskData taskData, TaskRunner taskRunner)
        {
            this.taskRunner = taskRunner;
            character = taskData.character;
            targets = taskData.targets;
            _ticksToPerform = taskData.ticksToPerform;
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
            }
            else
            {
                if (character.position == targets.currentPosition)
                {
                    _inRange = true;
                    return;
                }

                character.movement.Move(this);
            }
        }

        private void Run()
        {
            if (Perform())
            {
                End();
            }
        }

        public virtual void End()
        {
            taskStatus = TaskStatus.Success;
            taskRunner.EndTask();
        }

        public virtual void Start()
        {
            taskStatus = TaskStatus.Running;
        }

        public virtual bool Perform()
        {
            if (_ticksToPerform <= 0)
            {
                return true;
            }

            _ticks++;
            if (_ticks >= _ticksToPerform)
            {
                return true;
            }

            return false;
        }
    }
}