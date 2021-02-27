using System.Collections.Generic;
using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.World;
using Unity.Mathematics;
using UnityEngine;

namespace StandWorld.Visuals
{
    public class RegionRenderer 
    {
        public MapRegion region { get; protected set; }

        public Layer layer { get; protected set; }
        
        public Dictionary<int, MeshData> meshes { get; protected set; }

        private Vector3 _position;

        private bool _redraw = true;
        
        public RegionRenderer(MapRegion region, Layer layer)
        {
            this.region = region;
            this.layer = layer;
            meshes = new Dictionary<int, MeshData>();
            _position = new Vector3(0,0, LayerUtils.Height(layer));
        }

        public MeshData GetMesh(int graphicInstance, bool useSize = true)
        {
            if (meshes.ContainsKey(graphicInstance))
            {
                return meshes[graphicInstance];
            }

            if (useSize)
            {
                meshes.Add(graphicInstance, new MeshData(region.regionRect.area));
            }
            else
            {
                meshes.Add(graphicInstance, new MeshData());
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
                    quaternion.identity,
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

        public void BuildMeshes()
        {
            foreach (Vector2Int v in region.regionRect)
            {
                Tilable tilable = region.map[v].GetTilable(layer);
                if (tilable != null)
                {
                    MeshData currentMesh = this.GetMesh(tilable.graphics.uId);
                    int vIndex = currentMesh.vertices.Count;
                    
                    currentMesh.vertices.Add(new Vector3(v.x, v.y));
                    currentMesh.vertices.Add(new Vector3(v.x, v.y + 1));
                    currentMesh.vertices.Add(new Vector3(v.x + 1, v.y + 1));
                    currentMesh.vertices.Add(new Vector3(v.x + 1, v.y));

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
