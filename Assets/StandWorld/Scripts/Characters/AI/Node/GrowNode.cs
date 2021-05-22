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
                Tilable tilable = WorldUtilsPlant.Instance.FieldNextToCut(character.position);

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
                Tilable tilable = WorldUtilsPlant.Instance.FieldNextTileToSow(character.position);

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
                Tilable tilable = WorldUtilsPlant.Instance.FieldNextTileToDirt(character.position);

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
            BrainNode cut = new BrainNodeCondition(WorldUtilsPlant.Instance.FieldHasPlantsToCut);
            BrainNode sow = new BrainNodeCondition(WorldUtilsPlant.Instance.FieldHasPlantsToSow);
            BrainNode dirt = new BrainNodeCondition(WorldUtilsPlant.Instance.FieldHasDirtToWork);

            cut.AddSubnode(new CutPlantsAtPosition());
            sow.AddSubnode(new SowPlantAtPosition());
            dirt.AddSubnode(new DirtAtPosition());
            
            subNodes.Add(cut);
            subNodes.Add(sow);
            subNodes.Add(dirt);
        }
    }
}