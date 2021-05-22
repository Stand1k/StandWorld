using System;
using StandWorld.Definitions;
using StandWorld.World;
using UnityEngine;

namespace StandWorld.Characters.AI.Node
{
    public class HaulRecipeNode : BrainNodeCondition
    {
        private class HaulRecipe : BrainNode
        {
            public override Task GetTask()
            {
                TargetList targets = WorldUtilsBuidling.Instance.RecipesToComplete(30, character);
                
                if (targets != null)
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