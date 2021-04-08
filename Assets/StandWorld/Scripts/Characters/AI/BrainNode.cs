using System;
using System.Collections.Generic;

namespace StandWorld.Characters.AI
{
    public class BrainNode
    {
        public List<BrainNode> subNodes = new List<BrainNode>();
        public BaseCharacter character { get; protected set; }

        public void SetCharacter(BaseCharacter character)
        {
            this.character = character;
            foreach (BrainNode node in subNodes)
            {
                node.SetCharacter(character);
            }
        }

        public virtual TaskData GetTask()
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
        public override TaskData GetTask()
        {
            foreach (BrainNode node in subNodes)
            {
                TaskData taskData = node.GetTask();
                if (taskData != null)
                {
                    return taskData;
                }
            }

            return null;
        }
    }

    public class BrainNodeConditional : BrainNodePriority
    {
        public Func<bool> condition { get; protected set; }

        public BrainNodeConditional(Func<bool> condition)
        {
            this.condition = condition;
        }

        public override TaskData GetTask()
        {
            if (condition())
            {
                return base.GetTask();
            }

            return null;
        }
    }
}