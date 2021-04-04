using System;
using StandWorld.Definitions;
using StandWorld.Visuals;
using StandWorld.World;

namespace StandWorld.Characters.AI.Node
{
    public class EatVegiesNode : BrainNodeConditional
    {
        private class EatVegiesTaskData : BrainNode
        {
            public override TaskData GetTaskData()
            {
                BucketResult bucketResult = WorldUtils.HasVegetalNutrimentsInBucket(character.position);

                if (bucketResult.result)
                {
                    return new TaskData(
                        Defs.tasks["task_eat"],
                        new TargetList(new Target(bucketResult.tilable)),
                        character
                    );
                }

                return null;
            }
        }

        public EatVegiesNode(Func<bool> condition) : base(condition)
        {
            subNodes.Add(new EatVegiesTaskData());
        }
    }
}