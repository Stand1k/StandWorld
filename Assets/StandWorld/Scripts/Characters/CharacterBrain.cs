using StandWorld.Characters.AI;
using UnityEngine;

namespace StandWorld.Characters
{
    public class CharacterBrain
    {
        public BaseCharacter character { get; protected set; }
        public BrainNode brainNode { get; protected set; }
        public TaskRunner taskRunner { get; protected set; }
        public TaskData currentTaskData { get; protected set; }

        public CharacterBrain(BaseCharacter character, BrainNode brainNode)
        {
            this.character = character;
            this.brainNode = brainNode;
            this.brainNode.SetCharacter(character);
            taskRunner = new TaskRunner();
            currentTaskData = null;

            taskRunner.onEndTask = delegate
            {
                if (taskRunner.task.taskStatus == TaskStatus.Success)
                {
                    Debug.Log("Clearing Task Success");
                    currentTaskData = null;
                }
                else if (taskRunner.task.taskStatus == TaskStatus.Failed)
                {
                    Debug.Log("Clearing Task Failed");
                    currentTaskData = null;
                }
            };
        }

        public void Update()
        {
            if (currentTaskData == null)
            {
                GetNextTaskData();
            }
            else
            {
                if (taskRunner.running == false)
                {
                    Debug.Log("Starting new Task: " + currentTaskData.def.uID);
                    taskRunner.StartTask(currentTaskData);
                }
                else
                {
                    if (taskRunner.task.taskStatus == TaskStatus.Running)
                    {
                        taskRunner.task.Update();
                    }
                }
            }
        }

        public void GetNextTaskData()
        {
            TaskData nextTaskData = brainNode.GetTaskData();

            if (nextTaskData != null)
            {
                currentTaskData = nextTaskData;
            }
        }
    }
}