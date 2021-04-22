using System;
using System.Collections.Generic;
using UnityEngine;

namespace StandWorld.Characters.AI
{
    public abstract class BrainNode
    {
        public readonly List<BrainNode> subNodes = new List<BrainNode>();
        public BaseCharacter character { get; protected set; }

        public void SetCharacter(BaseCharacter character)
        {
            this.character = character;
            foreach (BrainNode node in subNodes)
            {
                node.SetCharacter(character);
            }
        }

        public virtual Task GetTask()
        {
            return null;
        }

        public BrainNode AddSubnode(BrainNode node)
        {
            subNodes.Add(node);
            return this;
        }
    }

    public class BrainNodePriority : BrainNode
    {
        public override Task GetTask()
        {
            foreach (BrainNode node in subNodes)
            {
                Task task = node.GetTask();

                if (task != null)
                {
                    return task;
                }
            }

            return null;
        }
    }

    public class BrainNodeCondition : BrainNodePriority
    {
        public Func<bool> condition { get; protected set; }

        public BrainNodeCondition(Func<bool> condition)
        {
            this.condition = condition;
        }

        public override Task GetTask()
        {
            if (condition())
            {
                return base.GetTask();
            }

            return null;
        }
    }
}