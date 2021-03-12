using System.Collections.Generic;
using StandWorld.Helpers;
using UnityEngine;

namespace StandWorld.Visuals
{
    public static class MeshPool 
    {
        public static Dictionary<float, MeshData> planes = new Dictionary<float, MeshData>();
        public static Dictionary<int, MeshData> cornerPlanes = new Dictionary<int, MeshData>();

        public static Mesh GetCornersPlane(bool[] corners)
        {
            int id = HashUtils.HashArrayValue<bool>(corners);

            if (cornerPlanes.ContainsKey(id))
            {
                return cornerPlanes[id].mesh;
            }
            
            cornerPlanes.Add(id, GenCornersPlane(corners));
            return cornerPlanes[id].mesh;
        }

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

        public static MeshData GenCornersPlane(bool[] corners)
        {
            MeshData meshData = new MeshData(4, (MeshFlags.Base | MeshFlags.UV));

            for (int i = 0; i < 4; i++)
            {
                if (corners[i])
                {
                    Vector2 loc = Vector2.zero;
                    float sx = 0.42f;
                    float sy = 0.55f;

                    if (i == 1)
                    {
                        sy = 0.42f;
                        loc.y = (1 - sy);
                    }
                    else if (i == 2)
                    {
                        sy = 0.42f;
                        loc.x = (1 - sx);
                        loc.y = (1 - sy);
                    }
                    else if (i == 3)
                    {
                        loc.x = (1 - sx);
                    }

                    int vIndex = meshData.vertices.Count;
                    meshData.vertices.Add(new Vector3(loc.x,loc.y));
                    meshData.vertices.Add(new Vector3(loc.x,loc.y + sy));
                    meshData.vertices.Add(new Vector3(loc.x + sx,loc.y + sy));
                    meshData.vertices.Add(new Vector3(loc.x + sx,loc.y));
                    meshData.UVs.Add(new Vector2(0f,0f));
                    meshData.UVs.Add(new Vector2(0f,1f));
                    meshData.UVs.Add(new Vector2(1f,1f));
                    meshData.UVs.Add(new Vector2(1f, 0f));
                    meshData.AddTriangle(vIndex,0,1,2);
                    meshData.AddTriangle(vIndex,0,2,3);
                }
            }
            
            meshData.Build();
            return meshData;
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
