using StandWorld.Characters.AI;
using UnityEngine;

namespace StandWorld.Characters
{
    public class CharacterBrain
    {
        public BaseCharacter character { get; protected set; }
        public BrainNode brainNode { get; protected set; }
        public Task currentTask;

        public CharacterBrain(BaseCharacter character, BrainNode brainNode)
        {
            this.character = character;
            this.brainNode = brainNode;
            this.brainNode.SetCharacter(character);
            currentTask = null;
        }

        public void Update()
        {
            if (currentTask == null)
            {
                GetNextTask();
            }
            else
            {
                if (currentTask.taskBase == null)
                {
                    currentTask.GetTaskClass(character);
                    return;
                }
                
                if (currentTask.state == TaskState.Success)
                {
                    currentTask = null;
                }
                else if (currentTask.state == TaskState.Failed)
                {
                    currentTask = null;
                }
                else
                {
                    currentTask.taskBase.Tick();
                }
            }
        }

        public void GetNextTask()
        {
            Task nextTask = brainNode.GetTask();
            if (nextTask != null)
            {
                StartNextTask(nextTask);
            }
        }

        public void StartNextTask(Task nextTask)
        {
            currentTask = nextTask;
        }
    }
}