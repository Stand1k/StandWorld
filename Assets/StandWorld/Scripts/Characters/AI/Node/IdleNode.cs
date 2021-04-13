using StandWorld.Definitions;
using UnityEngine;

namespace StandWorld.Characters.AI.Node
{
    public class IdleNodeTaskData : BrainNode
    {
        public override TaskData GetTaskData()
        {
            return new TaskData(
                Defs.tasks["task_idle"],
                new TargetList(Target.GetRandomTargetInRange(character.position)),
                character,
                Random.Range(100, 250)
            );
        }
    }
}