using System.Collections.Generic;
using UnityEngine;
using  StandWorld.Definitions;
using StandWorld.Visuals;

namespace StandWorld.Entities
{
    public class Tilable 
    {
        public Vector2Int position { get; protected set; }
        
        public TilableDef def { get; protected set; }
        
        public GraphicInstance mainGraphic { get; protected set; }
        
        public Dictionary<string, GraphicInstance> addGraphics { get; protected set; }

        private Dictionary<int, Matrix4x4> _matrices;

        public Matrix4x4 GetMatrice(int graphicUId)
        {
            if (_matrices == null)
            {
                _matrices = new Dictionary<int, Matrix4x4>();
            }

            if (!_matrices.ContainsKey(graphicUId))
            {
                Matrix4x4 mat = Matrix4x4.identity;
                
                mat.SetTRS(
                    new Vector3(
                        position.x - def.graphics.pivot.x,
                        position.y - def.graphics.pivot.y,
                        LayerUtils.Height(def.layer) + GraphicInstance.instances[graphicUId].drawPriority
                    ),
                    Quaternion.identity,
                    Vector3.one
                );

                _matrices.Add(graphicUId, mat);
            }

            return _matrices[graphicUId];
            
        }
    }
}
