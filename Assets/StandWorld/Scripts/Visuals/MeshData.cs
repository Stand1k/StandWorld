using System.Collections.Generic;
using UnityEngine;

namespace StandWorld.Visuals
{
    [System.Flags]
    public enum MeshFlags
    {
        Base = 1 << 0,
        UV = 1 << 1,
        Color = 1 << 2,
        ALl = ~(~0 << 3)
    }
    public class MeshData
    {
        public List<Vector3> vertices;

        public List<int> indices;

        public List<Vector2> UVs;

        public List<Color> colors;

        public Mesh mesh;

        private MeshFlags _flags;

        public MeshData(MeshFlags flags = MeshFlags.Base)
        {
            vertices = new List<Vector3>();
            indices = new List<int>();
            colors = new List<Color>();
            UVs = new List<Vector2>();
            _flags = flags;
        }

        public MeshData(int planeCount, MeshFlags flags = MeshFlags.Base)
        {
            vertices = new List<Vector3>(planeCount * 4);
            indices = new List<int>(planeCount * 6);
            colors = new List<Color>((flags & MeshFlags.Color) == MeshFlags.Color ? planeCount * 4 : 0);
            UVs = new List<Vector2>((flags & MeshFlags.UV) == MeshFlags.UV ? planeCount * 4 : 0);
            _flags = flags;
        }

        public void AddTriangle(int vIndex, int a, int b, int c)
        {
            indices.Add(vIndex + a);
            indices.Add(vIndex + b);
            indices.Add(vIndex + c);
        }

        public void CreateNewMesh()
        {
            if (mesh != null)
            {
                Object.Destroy(mesh);
            }

            mesh = new Mesh();
        }

        public void Clear()
        {
            vertices.Clear();
            indices.Clear();
            colors.Clear();
            UVs.Clear();
        }

        public Mesh Build()
        {
            CreateNewMesh();
            //Перевірка чи взагалі подрібно створювати Mesh
            if (vertices.Count > 0 && indices.Count > 0)
            {
                mesh.SetVertices(vertices);
                mesh.SetTriangles(indices, 0);

                if ((_flags & MeshFlags.UV) == MeshFlags.UV)
                {
                    mesh.SetUVs(0, UVs);
                }
                
                if ((_flags & MeshFlags.Color) == MeshFlags.Color)
                {
                    mesh.SetColors(colors);
                }
                
                Object.Destroy(mesh);
                return mesh;
            }

            return null;
        }
    }
}
