using UnityEngine;

namespace StandWorld.Definitions
{
    [System.Serializable]
    public class Def 
    {
        // Унікальний ідентифікатор
        public string uID { get; set; }

        public override int GetHashCode()
        {
            return this.uID.GetHashCode();
        }
    }

    [System.Serializable]
    public class GraphicDef : Def
    {
        public string textureName;
        public string materialName = "tilable";
        public Vector2 size = Vector2.one;
        public Color color = Color.white;
    }

    [System.Serializable]
    public class TilableDef : Def
    {
        public Layer layer;
        //Graphic data(size, texture, shader/material)
        public GraphicDef graphics;
    }

    [System.Serializable]
    public class GroundDef : TilableDef
    {
        
    }
}
