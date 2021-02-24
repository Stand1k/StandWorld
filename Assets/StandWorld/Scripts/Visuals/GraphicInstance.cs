using System.Collections.Generic;
using StandWorld.Definitions;
using StandWorld.Helpers;
using UnityEngine;

namespace StandWorld.Visuals
{
    public class GraphicInstance : MonoBehaviour
    {
        public static Dictionary<int, GraphicInstance> instances = new Dictionary<int, GraphicInstance>();
        
        public int uId { get; protected set; }
        public Material material { get; protected set; }
        public Texture2D texture { get; protected set; }
        public Color color { get; protected set; }
        public GraphicDef def { get; protected set; }

        public GraphicInstance(int uId, GraphicDef def)
        {
            this.def = def;
            this.uId = uId;
            material = new Material(Res.materials[def.materialName]);
            material.mainTexture = Res.textures[def.textureName];
        }

        public static GraphicInstance GetNew(GraphicDef def)
        {
            int id = GetUId(def);
            if(instances.ContainsKey(id))
            {
                return instances[id];
            }
            instances.Add(id, new GraphicInstance(id, def));
            return instances[id];
        }

        public override int GetHashCode()
        {
            return uId;
        }

        public static int GetUId(GraphicDef def)
        {
            return def.materialName.GetHashCode() + def.textureName.GetHashCode() + def.color.GetHashCode();
        }
    }
}
