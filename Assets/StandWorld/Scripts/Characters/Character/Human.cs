using StandWorld.Characters.AI;
using StandWorld.Characters.AI.Node;
using StandWorld.Definitions;
using StandWorld.World;
using UnityEngine;

namespace StandWorld.Characters
{
    public class Human : BaseCharacter
    {
        public HumanSkin humanSkin { get; protected set; }

        public Human(Vector2Int position, AnimalDef def, CharacterStats stats) : base(position, def, stats)
        {
            humanSkin = new HumanSkin(this, new Vector2(1.5f, 2.25f));
            movement.onChangeDirection += humanSkin.UpdateLookingAt;
        }

        public override BrainNodePriority GetBrainNode()
        {
            BrainNodePriority brainNode = new BrainNodePriority();

            brainNode
                .AddSubnode(new CutNode(WorldUtilsPlant.Instance.HasPlantToCut))
                .AddSubnode(new HaulRecipeNode(WorldUtilsBuidling.Instance.HaulRecipeNeeded))
                .AddSubnode(new GrowNode(WorldUtilsPlant.Instance.FieldHasWork))
                .AddSubnode(new SleepNode(() => stats.vitals[Vitals.Energy].ValueInfToPercent(0.2f)))
                //.AddSubnode(new EatVegiesNode( () => stats.vitals[Vitals.Hunger].ValueInfToPercent(0.25f)))
                .AddSubnode(new IdleNodeTaskData());
            
            return brainNode;
        }

        public override void UpdateDraw()
        {
            humanSkin.UpdateDraw();
        }
    }
}