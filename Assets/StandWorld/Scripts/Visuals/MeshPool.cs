using System.Collections.Generic;
using UnityEngine;

namespace StandWorld.Visuals
{
    public static class MeshPool 
    {
        public static Dictionary<float, MeshData> planes = new Dictionary<float, MeshData>();

        public static Mesh GetPlaneMesh(Vector2 size)
        {
            float id = (int) (size.x + size.y * 2121f);

            if (planes.ContainsKey(id))
            {
                return planes[id].mesh;
            }
            
            planes.Add(id, GenPlaneMesh(size));
            return planes[id].mesh;
        }

        public static MeshData GenPlaneMesh(Vector2 size)
        {
            MeshData meshData = new MeshData(1, (MeshFlags.Base | MeshFlags.UV));
            meshData.vertices.Add(new Vector3(0,0));
            meshData.vertices.Add(new Vector3(0,size.y));
            meshData.vertices.Add(new Vector3(size.x,size.y));
            meshData.vertices.Add(new Vector3(size.x,0));
            meshData.UVs.Add(new Vector2(0f,0f));
            meshData.UVs.Add(new Vector2(0f,1f));
            meshData.UVs.Add(new Vector2(1f,1f));
            meshData.UVs.Add(new Vector2(1f, 0f));
            meshData.AddTriangle(0,0,1,2);
            meshData.AddTriangle(0,0,2,3);
            meshData.Build();

            return meshData;
        }
    }
}
