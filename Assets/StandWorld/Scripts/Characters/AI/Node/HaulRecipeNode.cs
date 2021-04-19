using System;
using StandWorld.Definitions;
using StandWorld.World;
using UnityEngine;

namespace StandWorld.Characters.AI.Node
{
    public class HaulRecipeNode : BrainNodeConditional
    {
        private class HaulRecipe : BrainNode
        {
            public override Task GetTask()
            {
                TargetList targets = WorldUtils.RecipesToComplete(10, character);
                if (targets != null && targets.targets.Count != 0)
                {
                    return new Task(
                        Defs.tasks["haul_recipe"],
                        targets
                    );
                }

                return null;
            }
        }

        public HaulRecipeNode(Func<bool> condition) : base(condition)
        {
            subNodes.Add(new HaulRecipe());
        }
    }
}