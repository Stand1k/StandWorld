using System.Collections.Generic;
using UnityEngine;

namespace StandWorld.Helpers
{
    public static class Res
    {
        public static Dictionary<string, Material> materials;
        public static Dictionary<string, Texture2D> textures;

        public static void Load()
        {
            Res.materials = new Dictionary<string, Material>();
            foreach (Material mat in Resources.LoadAll<Material>("Materials/"))
            {
                Res.materials.Add(mat.name, mat);
            }
            
            Res.textures = new Dictionary<string, Texture2D>();
            foreach (Texture2D texture in Resources.LoadAll<Texture2D>("Textures/"))
            {
                Res.textures.Add(texture.name, texture);
            }
        }
    }
}
