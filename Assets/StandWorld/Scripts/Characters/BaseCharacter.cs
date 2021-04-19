using System.Collections.Generic;
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
        public CharacterStats stats { get; protected set; }
        public LivingDef def { get; protected set; }
        public GraphicInstance graphics { get; protected set; }
        public new Vector2Int position => movement.position;
        public CharacterMovement movement { get; protected set; }
        public CharacterBrain brain { get; protected set; }
        public Inventory inventory { get; protected set; }

        public string name { get; protected set; }

        private Mesh _mesh;

        public BaseCharacter(Vector2Int position, LivingDef def, CharacterStats stats)
        {
            this.stats = stats;
            this.def = def;
            movement = new CharacterMovement(position, this);
            brain = new CharacterBrain(this, GetBrainNode());
            inventory = new Inventory(30);
            name = SetName();

            if (this.def.graphics != null && def.graphics.textureName != string.Empty)
            {
                graphics = GraphicInstance.GetNew(this.def.graphics);
            }

            ToolBox.tick.toAdd.Enqueue(Update);
        }

        public virtual string SetName()
        {
            return "Chel " + Random.Range(1, 1000);
        }

        public abstract BrainNodePriority GetBrainNode();

        public void DropOnTheFloor()
        {
            if (inventory.count > 0 && inventory.def != null)
            {
                HashSet<Vector2Int> tilablesInRadius = new HashSet<Vector2Int>();
                Stackable stack = (Stackable) ToolBox.map.GetTilableAt(this.position, Layer.Stackable);
                if (stack == null)
                {
                    ToolBox.map.Spawn(position, new Stackable(
                        position,
                        inventory.def,
                        0
                    ));
                }

                stack = (Stackable) ToolBox.map.GetTilableAt(this.position, Layer.Stackable);
                Tilable.InRadius(20, stack.position, stack.position, ref tilablesInRadius);
                
                foreach (Vector2Int position in tilablesInRadius)
                {
                    if (inventory.count == 0)
                    {
                        break;
                    }

                    stack = (Stackable) ToolBox.map.GetTilableAt(position, Layer.Stackable);
                    if (stack != null && stack.tilableDef == inventory.def)
                    {
                        inventory.TransfertTo(stack.inventory, stack.inventory.free);
                    }
                    else if (stack == null)
                    {
                        ToolBox.map.Spawn(position, new Stackable(
                            position,
                            inventory.def,
                            0
                        ));
                        stack = (Stackable) ToolBox.map.GetTilableAt(position, Layer.Stackable);
                        inventory.TransfertTo(stack.inventory, stack.inventory.free);
                    }
                }
            }
        }

        public virtual void Update()
        {
            brain.Update();
            stats.Update();
        }

        public virtual void UpdateDraw()
        {
            if (def.graphics == null)
            {
                return;
            }

            if (_mesh == null)
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