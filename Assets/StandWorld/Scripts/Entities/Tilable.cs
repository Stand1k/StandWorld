using UnityEngine;
using  StandWorld.Definitions;
using StandWorld.Visuals;

namespace StandWorld.Entities
{
    public class Tilable 
    {
        public Vector2Int position { get; protected set; }
        
        public TilableDef def { get; protected set; }
        
        public  GraphicInstance graphics { get; protected set; }

        private Matrix4x4 _matrice;

        public Matrix4x4 GetMatrice()
        {
            if (_matrice == default(Matrix4x4))
            {
                _matrice = Matrix4x4.identity;
                _matrice.SetTRS(
                    new Vector3(
                        position.x - graphics.def.pivot.x,
                        position.y - graphics.def.pivot.y,
                        LayerUtils.Height(def.layer) + graphics.drawPriority
                        ),
                Quaternion.identity,
                    Vector3.one
                    );
            }

            return _matrice;
        }
    }
}
