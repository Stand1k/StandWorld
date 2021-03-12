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
        public Color32 color { get; protected set; }
        public Mesh mesh { get; protected set; }
        public GraphicDef def { get; protected set; }
        public float drawPriority { get; protected set; }

        public GraphicInstance(int uId, GraphicDef def, Mesh mesh, Color32 color = default(Color32), Texture2D texture = null, float drawPriority = -21f)
        {
            this.mesh = mesh;
            this.def = def;
            this.uId = uId;
            material = new Material(Res.materials[def.materialName]);
            material.mainTexture = texture;
            this.drawPriority = drawPriority / -100f;

            if (color != default(Color))
            {
                SetColor(color);
            }
        }

        private void SetColor(Color32 color)
        {
            this.color = color;
            material.SetColor("_Color", this.color);
        }

        public static GraphicInstance GetNew(GraphicDef def, Color32 color = default(Color32), Texture2D texture = null, float drawPriority = -21f, Mesh mesh = null)
        {
            Mesh _mesh = (mesh == null) ? MeshPool.GetPlaneMesh(def.size) : mesh;
            Color _color = (color == default(Color)) ? def.color : (Color) color; 
             Texture2D _texture = (texture == null) ? Res.textures[def.textureName] : texture;
            float _drawPriority = (drawPriority == -21f) ? def.drawPriority : drawPriority;
            
            int id = GetUId(def, _color, _texture, _drawPriority, _mesh);
            if(instances.ContainsKey(id))
            {
                return instances[id];
            }
            instances.Add(id, new GraphicInstance(id, def, _mesh, _color, _texture, _drawPriority));
            return instances[id];
        }
        
        public static int GetUId(GraphicDef def, Color color, Texture2D texture, float drawPriority, Mesh mesh)
        {
            
            
            return def.materialName.GetHashCode() + texture.GetHashCode() + color.GetHashCode() + drawPriority.GetHashCode() + mesh.GetHashCode();
        }

        public override int GetHashCode()
        {
            return uId;
        }
    }
}
