using StandWorld.Characters.AI;
using StandWorld.Characters.AI.Node;
using StandWorld.Definitions;
using UnityEngine;

namespace StandWorld.Characters
{
    public class Animal : BaseCharacter
    {
        public Animal(Vector2Int position, LivingDef def, CharacterStats stats) : base(position, def, stats)
        {
        }

        public override BrainNodePriority GetBrainNode()
        {
            BrainNodePriority brainNode = new BrainNodePriority();

            brainNode.AddSubnode(new SleepNode(
                    () => stats.vitals[Vitals.Energy].ValueInfToPercent(0.2f))
                )
                .AddSubnode(new EatVegiesNode(
                    () => stats.vitals[Vitals.Hunger].ValueInfToPercent(0.25f))
                )
                .AddSubnode(new IdleNodeTaskData());
            
            return brainNode;
        }
    }
}