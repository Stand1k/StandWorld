﻿using StandWorld.Definitions;
using StandWorld.Game;
using StandWorld.Visuals;
using UnityEngine;

namespace StandWorld.Characters
{
    public abstract class BaseCharacter 
    {
        public BaseStats stats { get; protected set; }
        public LivingDef def { get; protected set; }
        public GraphicInstance graphics { get; protected set; }
        
        public CharacterMovement movement { get; protected set; }
        
        public Vector2Int position { get; protected set; }

        private Mesh _mesh;

        public BaseCharacter(Vector2Int position, LivingDef def)
        {
            stats = new BaseStats();
            this.def = def;
            movement = new CharacterMovement(position);

            if (this.def.graphics != null)
            {
                graphics = GraphicInstance.GetNew(this.def.graphics);
            }
            
            ToolBox.tick.toAdd.Enqueue(Update);
        }

        public virtual void Update()
        {
            movement.Move(new Vector2Int(Random.Range(15,35), Random.Range(15,35)));
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

    public class Animal : BaseCharacter
    {
        public Animal(Vector2Int position, LivingDef def) : base(position, def)
        {
        }
    }
}
