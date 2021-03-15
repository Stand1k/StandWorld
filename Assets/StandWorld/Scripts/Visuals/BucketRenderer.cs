using System.Collections.Generic;
using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.World;
using Unity.Mathematics;
using UnityEngine;

namespace StandWorld.Visuals
{
    public class BucketRenderer 
    {
        public LayerBucketGrid bucket { get; protected set; }

        public Layer layer { get; protected set; }
        
        public Dictionary<int, MeshData> meshes { get; protected set; }

        private Vector3 _position;

        private bool _redraw = true;
        
        public BucketRenderer(LayerBucketGrid bucket, Layer layer)
        {
            this.bucket = bucket;
            this.layer = layer;
            meshes = new Dictionary<int, MeshData>();
            _position = new Vector3(0,0, 0);
        }

        public MeshData GetMesh(int graphicInstance, bool useSize = true, MeshFlags flags = MeshFlags.Base)
        {
            if (meshes.ContainsKey(graphicInstance))
            {
                return meshes[graphicInstance];
            }

            if (useSize)
            {
                meshes.Add(graphicInstance, new MeshData(bucket.rect.area, flags));
            }
            else
            {
                meshes.Add(graphicInstance, new MeshData(flags));
            }

            return meshes[graphicInstance];
        }

        public void Draw()
        {
            if (_redraw)
            {
                BuildMeshes();
                _redraw = false;
            }

            foreach (KeyValuePair<int, MeshData> kv in meshes)
            {
                Graphics.DrawMesh(
                    kv.Value.mesh,
                    _position, 
                    Quaternion.identity,
                    GraphicInstance.instances[kv.Key].material,
                    0
                    );
            }
            
        }

        public void ClearMeshes()
        {
            foreach (MeshData meshData in meshes.Values)
            {
                meshData.Clear();
            }
        }

        public virtual void BuildMeshes()
        {
            foreach (Tilable tilable in bucket.tilables)
            {
                if (tilable != null && !tilable.hidden)
                {
                    MeshData currentMesh = GetMesh(tilable.mainGraphic.uId);
                    int vIndex = currentMesh.vertices.Count;
                    
                    currentMesh.vertices.Add(new Vector3(tilable.position.x, tilable.position.y));
                    currentMesh.vertices.Add(new Vector3(tilable.position.x, tilable.position.y + 1));
                    currentMesh.vertices.Add(new Vector3(tilable.position.x + 1, tilable.position.y + 1));
                    currentMesh.vertices.Add(new Vector3(tilable.position.x + 1, tilable.position.y));

                    currentMesh.AddTriangle(vIndex, 0, 1, 2);
                    currentMesh.AddTriangle(vIndex, 0, 2, 3);
                }
            }

            foreach (MeshData meshData in meshes.Values)
            {
                meshData.Build();
            }
        }
    }
}
