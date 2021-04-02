using System;
using StandWorld.Definitions;

namespace StandWorld.Characters.AI.Node
{
    public class SleepNode : BrainNodeConditional
    {
        private class SleepNodeTaskData : BrainNode
        {
            public override TaskData GetTaskData()
            {
                return new TaskData(
                    Defs.tasks["task_sleep"],
                    new TargetList(Target.GetRandomTargetInRange(character.position)),
                    character
                );
            }
        }

        public SleepNode(Func<bool> condition) : base(condition)
        {
            subNodes.Add(new SleepNodeTaskData());
        }
    }
}