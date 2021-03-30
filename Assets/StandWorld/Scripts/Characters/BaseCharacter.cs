using StandWorld.Characters.AI;
using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.Game;
using StandWorld.Visuals;
using UnityEngine;

namespace StandWorld.Characters
{
    public abstract class BaseCharacter : Entity
    {
        public BaseStats stats { get; protected set; }
        public LivingDef def { get; protected set; }
        public GraphicInstance graphics { get; protected set; }
        public new Vector2Int position => movement.position;
        public CharacterMovement movement { get; protected set; }
        public TaskRunner taskRunner { get; protected set; }
        
        public string name { get; protected set; }

        private Mesh _mesh;

        public BaseCharacter(Vector2Int position, LivingDef def)
        {
            stats = new BaseStats();
            this.def = def;
            movement = new CharacterMovement(position, this);
            taskRunner = new TaskRunner();
            name = "Chel " + Random.Range(1, 1000);

            if (this.def.graphics != null)
            {
                graphics = GraphicInstance.GetNew(this.def.graphics);
            }
            
            ToolBox.tick.toAdd.Enqueue(Update);
        }

        public virtual void Update()
        {
            if (taskRunner.running == false || taskRunner.task.taskStatus == TaskStatus.Failed || taskRunner.task.taskStatus == TaskStatus.Success)
            {
                taskRunner.StartTask(Defs.tasks["task_idle"],this, new TargetList(Target.GetRandomTargetInRange(position)));
            }
            else
            {
                taskRunner.task.Update();
            }
            
            stats.Update();
        }
        
        public virtual void UpdateDraw()
        {
            if (def.graphics == null)
            {
                return;
            }
            
            if(_mesh == null)
            {
                _mesh = MeshPool.GetPlaneMesh(def.graphics.size);
            }

            Graphics.DrawMesh(
                _mesh,
                movement.visualPosition,
                Quaternion.identity,
                graphics.material,
                0
                );
        }
    }
}
