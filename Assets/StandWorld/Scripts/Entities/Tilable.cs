using System.Collections.Generic;
using StandWorld.Definitions;
using StandWorld.Visuals;
using StandWorld.World;
using UnityEngine;

namespace StandWorld.Entities
{
    public class Tilable 
    {
        public Vector2Int position { get; protected set; }
        
        public Vector3 scale = Vector3.one;
        
        public TilableDef def { get; protected set; }

        public GraphicInstance mainGraphic;

        public Dictionary<string, GraphicInstance> addGraphics;

        private Dictionary<int, Matrix4x4> _matrices;
        
        public bool resetMatrices;

        protected int ticks = 0;

        public bool hidden = false;
        
        public LayerBucketGrid bucket { get; protected set; }
        
        public void SetBucket(LayerBucketGrid bucket) {
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
                        - def.graphics.pivot.x * scale.x
                        + (1f - scale.x) / 2f
                        ,position.y
                         - def.graphics.pivot.y * scale.y
                         + (1f - scale.y) / 2f
                        ,LayerUtils.Height(def.layer) + GraphicInstance.instances[graphicUId].drawPriority 
                    ), 
                    Quaternion.identity,
                    scale
                );

                _matrices.Add(graphicUId, mat);
            }

            return _matrices[graphicUId];
            
        }
        
        public virtual void Destroy() {
            if (bucket != null) {
                bucket.DelTilable(this);
            }
        }
    }
}
