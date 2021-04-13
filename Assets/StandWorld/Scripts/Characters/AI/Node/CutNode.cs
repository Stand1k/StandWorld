using System;
using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.World;

namespace StandWorld.Characters.AI.Node
{
    public class CutNode : BrainNodeConditional
    {
        private class CutPlantAtPosition : BrainNode
        {
            public override TaskData GetTaskData()
            {
                Tilable tilable = WorldUtils.NextToCut(this.character.position);
                if (tilable != null)
                {
                    return new TaskData(
                        Defs.tasks["task_cut"],
                        new TargetList(new Target(tilable)),
                        this.character
                    );
                }

                return null;
            }
        }


        public CutNode(Func<bool> condition) : base(condition)
        {
            subNodes.Add(new CutPlantAtPosition());
        }
    }
}