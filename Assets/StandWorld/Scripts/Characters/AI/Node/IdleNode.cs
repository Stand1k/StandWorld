using StandWorld.Definitions;
using UnityEngine;

namespace StandWorld.Characters.AI.Node
{
    public class IdleNodeTaskData : BrainNode
    {
        public override Task GetTask()
        {
            return new Task(
                Defs.tasks["task_idle"],
                new TargetList(Target.GetRandomTargetInRange(character.position)),
                Random.Range(100, 200)
            );
        }
    }
}