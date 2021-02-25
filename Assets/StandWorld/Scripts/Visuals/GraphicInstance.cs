using System.Collections.Generic;
using StandWorld.Definitions;
using StandWorld.Helpers;
using UnityEngine;

namespace StandWorld.Visuals
{
    public class GraphicInstance 
    {
        public static Dictionary<int, GraphicInstance> instances = new Dictionary<int, GraphicInstance>();
        
        public int uId { get; protected set; }
        public Material material { get; protected set; }
        public Texture2D texture { get; protected set; }
        public Color color { get; protected set; }
        public Mesh mesh { get; protected set; }
        public GraphicDef def { get; protected set; }

        public GraphicInstance(int uId, GraphicDef def, Color color = default(Color), Texture2D texture = null)
        {
            this.def = def;
            this.uId = uId;
            material = new Material(Res.materials[def.materialName]);
            material.mainTexture = (texture == null) ? Res.textures[def.textureName] : texture;

            if (color != default(Color))
            {
                SetColor(color);
            }
        }

        private void SetColor(Color color)
        {
            this.color = color;
            this.material.SetColor("_Color", this.color);
        }

        public static GraphicInstance GetNew(GraphicDef def, Color color = default(Color), Texture2D texture = null)
        {
            int id = GetUId(def, color, texture);
            if(instances.ContainsKey(id))
            {
                return instances[id];
            }
            instances.Add(id, new GraphicInstance(id, def, color, texture));
            return instances[id];
        }

        public override int GetHashCode()
        {
            return uId;
        }

        public static int GetUId(GraphicDef def, Color color, Texture2D texture)
        {
            int textureHash = (texture == null) ? def.textureName.GetHashCode() : texture.GetHashCode();
            int colorHash = (color == default(Color)) ? def.color.GetHashCode() : color.GetHashCode();
            return def.materialName.GetHashCode() + textureHash + colorHash;
        }
    }
}
