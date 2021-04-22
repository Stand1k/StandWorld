using System;
using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.World;

namespace StandWorld.Characters.AI.Node
{
    public class GrowNode : BrainNodeCondition
    {
        private class CutPlantsAtPosition : BrainNode
        {
            public override Task GetTask()
            {
                Tilable tilable = WorldUtils.FieldNextToCut(character.position);

                if (tilable != null)
                {
                    return new Task(
                        Defs.tasks["task_cut"],
                        new TargetList(new Target(tilable))
                    );
                }

                return null;
            }
        }
        
        private class SowPlantAtPosition : BrainNode
        {
            public override Task GetTask()
            {
                Tilable tilable = WorldUtils.FieldNextTileToSow(character.position);

                if (tilable != null)
                {
                    return new Task(
                        Defs.tasks["task_sow"],
                        new TargetList(new Target(tilable))
                    );
                }

                return null;
            }
        }
        
        private class DirtAtPosition : BrainNode
        {
            public override Task GetTask()
            {
                Tilable tilable = WorldUtils.FieldNextTileToDirt(character.position);

                if (tilable != null)
                {
                    return new Task(
                        Defs.tasks["task_dirt"],
                        new TargetList(new Target(tilable))
                    );
                }

                return null;
            }
        }

        public GrowNode(Func<bool> condition) : base(condition)
        {
            BrainNode cut = new BrainNodeCondition(WorldUtils.FieldHasPlantsToCut);
            BrainNode sow = new BrainNodeCondition(WorldUtils.FieldHasPlantsToSow);
            BrainNode dirt = new BrainNodeCondition(WorldUtils.FieldHasDirtToWork);

            cut.AddSubnode(new CutPlantsAtPosition());
            sow.AddSubnode(new SowPlantAtPosition());
            dirt.AddSubnode(new DirtAtPosition());
            
            subNodes.Add(cut);
            subNodes.Add(sow);
            subNodes.Add(dirt);
        }
    }
}