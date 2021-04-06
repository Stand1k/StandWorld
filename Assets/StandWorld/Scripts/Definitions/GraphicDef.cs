using UnityEngine;

namespace StandWorld.Definitions
{
    [System.Serializable]
    public class GraphicDef : Def
    {
        public string textureName = string.Empty;
        public string materialName = "tilables";
        public Vector2 size = Vector2.one;
        public Vector2 pivot = Vector2.zero;
        public Color color = Color.white;
        public float drawPriority = 0f;
        public bool isInstanced = true;
    }
}