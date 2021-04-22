using System;
using StandWorld.Definitions;
using StandWorld.Visuals;
using StandWorld.World;
using UnityEngine;

namespace StandWorld.Characters.AI.Node
{
    public class EatVegiesNode : BrainNodeCondition
    {
        private class EatVegiesTaskData : BrainNode
        {
            public override Task GetTask()
            {
                BucketResult bucketResult = WorldUtils.HasVegetalNutrimentsInBucket(character.position);

                if (bucketResult.result)
                {
                    return new Task(
                        Defs.tasks["task_eat"],
                        new TargetList(new Target(bucketResult.tilable))
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