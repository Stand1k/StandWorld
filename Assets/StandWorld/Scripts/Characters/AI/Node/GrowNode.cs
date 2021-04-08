using System;
using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.World;

namespace StandWorld.Characters.AI.Node
{
    public class GrowNode : BrainNodeConditional
    {
        private class CutPlantsAtPosition : BrainNode
        {
            public override TaskData GetTask()
            {
                Tilable tilable = WorldUtils.FieldNextPlantToCut(character.position);

                if (tilable != null)
                {
                    return new TaskData(
                        Defs.tasks["task_cut"],
                        new TargetList(new Target(tilable)),
                        character
                    );
                }

                return null;
            }
        }
        
        private class HarvestPlantAtPosition : BrainNode
        {
            public override TaskData GetTask()
            {
                throw new NotImplementedException();
            }
        }
        
        private class SowPlantAtPosition : BrainNode
        {
            public override TaskData GetTask()
            {
                Tilable tilable = WorldUtils.FieldNextTileToSow(character.position);

                if (tilable != null)
                {
                    return new TaskData(
                        Defs.tasks["task_sow"],
                        new TargetList(new Target(tilable)),
                        character
                    );
                }

                return null;
            }
        }
        
        private class DirtAtPosition : BrainNode
        {
            public override TaskData GetTask()
            {
                Tilable tilable = WorldUtils.FieldNextTileToDirt(character.position);

                if (tilable != null)
                {
                    return new TaskData(
                        Defs.tasks["task_dirt"],
                        new TargetList(new Target(tilable)),
                        character
                    );
                }

                return null;
            }
        }

        public GrowNode(Func<bool> condition) : base(condition)
        {
            BrainNode cut = new BrainNodeConditional(WorldUtils.FieldHasPlantsToCut);
            //BrainNode harvers = new BrainNodeConditional(WorldUtils.FieldHasPlantsToHarvest);
            BrainNode sow = new BrainNodeConditional(WorldUtils.FieldHasPlantsToSow);
            BrainNode dirt = new BrainNodeConditional(WorldUtils.FieldHasDirtToWork);

            cut.AddSubnode(new CutPlantsAtPosition());
            //harvers.AddSubnode(new HarvestPlantAtPosition());
            sow.AddSubnode(new SowPlantAtPosition());
            dirt.AddSubnode(new DirtAtPosition());
            
            subNodes.Add(cut);
            //subNodes.Add(harvers);
            subNodes.Add(sow);
            subNodes.Add(dirt);
        }
    }
}