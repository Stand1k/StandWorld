using System;
using StandWorld.Definitions;

namespace StandWorld.Characters.AI.Node
{
    public class SleepNode : BrainNodeCondition
    {
        private class SleepNodeTaskData : BrainNode
        {
            public override Task GetTask()
            {
                return new Task(
                    Defs.tasks["task_sleep"],
                    new TargetList(Target.GetRandomTargetInRange(character.position))
                );
            }
        }

        public SleepNode(Func<bool> condition) : base(condition)
        {
            subNodes.Add(new SleepNodeTaskData());
        }
    }
}