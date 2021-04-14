using System.Collections.Generic;
using StandWorld.Definitions;
using StandWorld.Helpers;
using StandWorld.Visuals;
using StandWorld.World;
using UnityEngine;

namespace StandWorld.Entities
{
    public abstract class Entity
    {
        public Vector2Int position { get; protected set; }
    }

    public class Tilable : Entity
    {
        public Vector3 scale = Vector3.one;

        public TilableDef tilableDef { get; protected set; }

        public GraphicInstance mainGraphic;

        public Dictionary<string, GraphicInstance> addGraphics;

        private Dictionary<int, Matrix4x4> _matrices;

        public bool resetMatrices;

        protected int ticks = 0;

        public bool hidden = false;
        public LayerBucketGrid bucket { get; protected set; }
        public MenuOrderDef currentOrderDef { get; protected set; }

        public bool hasOrder => currentOrderDef != null;

        public void SetBucket(LayerBucketGrid bucket)
        {
            this.bucket = bucket;
        }

        public virtual void UpdateGraphics()
        {
        }

        public Matrix4x4 GetMatrice(int graphicUId)
        {
            if (_matrices == null || resetMatrices)
            {
                _matrices = new Dictionary<int, Matrix4x4>();
                resetMatrices = true;
            }

            if (!_matrices.ContainsKey(graphicUId))
            {
                Matrix4x4 mat = Matrix4x4.identity;

                mat.SetTRS(
                    new Vector3(
                        position.x
                        - tilableDef.graphics.pivot.x * scale.x
                        + (1f - scale.x) / 2f
                        , position.y
                          - tilableDef.graphics.pivot.y * scale.y
                          + (1f - scale.y) / 2f
                        , LayerUtils.Height(tilableDef.layer) + GraphicInstance.instances[graphicUId].drawPriority
                    ),
                    Quaternion.identity,
                    scale
                );

                _matrices.Add(graphicUId, mat);
            }

            return _matrices[graphicUId];
        }

        public virtual void AddOrder(MenuOrderDef def)
        {
            currentOrderDef = def;
            if (addGraphics == null)
            {
                addGraphics = new Dictionary<string, GraphicInstance>();
            }

            resetMatrices = true;
            UpdateOrderGraphics();
        }

        public virtual void ClearOrder()
        {
            if (hasOrder)
            {
                addGraphics.Remove(currentOrderDef.name);
                currentOrderDef = null;
            }
        }

        public virtual void UpdateOrderGraphics()
        {
            if (!addGraphics.ContainsKey(currentOrderDef.name))
            {
                addGraphics.Add(currentOrderDef.name,
                    GraphicInstance.GetNew(
                        currentOrderDef.graphicDef,
                        Color.white,
                        Res.textures[currentOrderDef.graphicDef.textureName],
                        42
                    )
                );
            }
        }

        public virtual void Destroy()
        {
            if (bucket != null)
            {
                bucket.DelTilable(this);
            }
        }
    }
}