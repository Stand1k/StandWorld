using System.Collections.Generic;
using UnityEngine;

namespace StandWorld.Definitions
{
    [System.Serializable]
    public class Def 
    {
        // Унікальний ідентифікатор
        public string uID;

        public override int GetHashCode()
        {
            return uID.GetHashCode();
        }
    }

    [System.Serializable]
    public class GraphicDef : Def
    {
        public string textureName;
        public string materialName = "tilables";
        public Vector2 size = Vector2.one;
        public Vector2 pivot = Vector2.zero;
        public Color32 color = Color.white;
        public float drawPriority = 0f;
        public bool isInstanced = true;
    }

    [System.Serializable]
    public class ColorPaletteDef : Def
    {
        public List<Color> colors = new List<Color>(15);

        public Color32 GetRandom()
        {
            return colors[Random.Range(0, colors.Count)];
        }
    }

    [System.Serializable]
    public class TilableDef : Def
    {
        public Layer layer;
        //Graphic data(size, texture, shader/material)
        public GraphicDef graphics;

        public GroundDef groundDef;

        public PlantDef plantDef;
    }

    [System.Serializable]
    public class GroundDef : Def
    {
        public float maxHeight;
    }
    
    [System.Serializable]
    public class PlantDef : Def
    {
        
    }
}
