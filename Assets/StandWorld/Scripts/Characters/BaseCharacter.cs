﻿using StandWorld.Characters.AI;
using StandWorld.Characters.AI.Node;
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
        public InventoryTilable inventory { get; protected set; }
        
        public string name { get; protected set; }

        private Mesh _mesh;

        public BaseCharacter(Vector2Int position, LivingDef def)
        {
            stats = new HumanStats(); //TODO: Переробити шоб все було через UpCast
            this.def = def;
            movement = new CharacterMovement(position, this);
            brain = new CharacterBrain(this, GetBrainNode());
            inventory = new InventoryTilable();
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
