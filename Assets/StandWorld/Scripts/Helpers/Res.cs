using System.Collections.Generic;
using UnityEngine;

namespace StandWorld.Helpers
{
    public static class Res
    {
        public static Dictionary<string, Material> materials;
        public static Dictionary<string, Texture2D> textures;
        public static Dictionary<string, GameObject> prefabs;
        public static Dictionary<Color, Texture2D> unicolorTextures = new Dictionary<Color, Texture2D>();
        public static GUISkin defaultGUI;

        public static void Load()
        {
            defaultGUI = Resources.Load<GUISkin>("UI/defaultGUI");

            materials = new Dictionary<string, Material>();
            foreach (Material mat in Resources.LoadAll<Material>("Materials/"))
            {
                materials.Add(mat.name, mat);
            }

            prefabs = new Dictionary<string, GameObject>();
            foreach (GameObject prefab in Resources.LoadAll<GameObject>("Prefabs/"))
            {
                prefabs.Add(prefab.name, prefab);
            }

            textures = new Dictionary<string, Texture2D>();
            foreach (Texture2D text in Resources.LoadAll<Texture2D>("Textures/"))
            {
                textures.Add(text.name, text);
            }
        }

        public static Texture2D TextureUnicolor(Color color)
        {
            if (unicolorTextures.ContainsKey(color))
            {
                return unicolorTextures[color];
            }

            Texture2D text = new Texture2D(1, 1);
            text.SetPixel(0, 0, color);
            text.Apply();
            unicolorTextures.Add(color, text);
            return text;
        }
    }
}