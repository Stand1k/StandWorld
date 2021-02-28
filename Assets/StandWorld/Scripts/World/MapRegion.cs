﻿using System.Collections.Generic;
using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.Helpers;
using StandWorld.Visuals;
using UnityEngine;

namespace StandWorld.World
{
    public class MapRegion 
    {
        public RectI regionRect { get; protected set; }

        public Map map { get; protected set; }
        
        public int id { get; protected set; }
        
        public Dictionary<Layer, RegionRenderer> renderers { get; protected set; }

        private Dictionary<int, Matrix4x4[]> _matrices;
        private bool _needToRefreshMatrices = true;

        public MapRegion(int id, RectI regionRect, Map map)
        {
            this.id = id;
            this.regionRect = regionRect;
            this.map = map;
            this.renderers = new Dictionary<Layer, RegionRenderer>();
            AddRenderers();
        }

        public void BuildMatrices()
        {
            Dictionary<int, List<Matrix4x4>> tmpMatrices = new Dictionary<int, List<Matrix4x4>>();

            foreach (Vector2Int v in regionRect)
            {
                foreach (Tilable t in map[v].GetAllTilables())
                {
                    if (t.def.graphics.isInstanced)
                    {
                        if (!tmpMatrices.ContainsKey(t.graphics.uId))
                        {
                            tmpMatrices.Add(t.graphics.uId, new List<Matrix4x4>());
                        }
                        tmpMatrices[t.graphics.uId].Add(t.GetMatrice());
                    }
                }
            }

            _matrices = new Dictionary<int, Matrix4x4[]>();

            foreach (int id in tmpMatrices.Keys)
            {
                _matrices.Add(id, new Matrix4x4[tmpMatrices[id].Count]);
                tmpMatrices[id].CopyTo(_matrices[id]);
            }
        }

        public void Draw()
        {
            foreach (RegionRenderer regionRenderer in renderers.Values)
            {
                regionRenderer.Draw();
            }

            DrawMatrices();
        }

        public bool IsVisible()
        {
            return(
                    regionRect.min.x >= ToolBox.cameraController.viewRect.min.x - Map.REGION_SIZE && 
                    regionRect.max.x <= ToolBox.cameraController.viewRect.max.x + Map.REGION_SIZE && 
                    regionRect.min.y >= ToolBox.cameraController.viewRect.min.y - Map.REGION_SIZE &&
                    regionRect.max.y <= ToolBox.cameraController.viewRect.max.y + Map.REGION_SIZE
            );
        }

        private void DrawMatrices()
        {
            if (_needToRefreshMatrices)
            {
                BuildMatrices();
                _needToRefreshMatrices = false;
            }

            foreach (KeyValuePair<int, Matrix4x4[]> kv in _matrices)
            {
                Graphics.DrawMeshInstanced(
                    MeshPool.GetPlaneMesh(GraphicInstance.instances[kv.Key].def.size),
                    0,
                    GraphicInstance.instances[kv.Key].material,
                    kv.Value
                );
            }
        }

        public void BuildMeshes()
        {
            foreach (RegionRenderer regionRenderer in renderers.Values)
            {
                regionRenderer.BuildMeshes();
            }
        }

        private void AddRenderers()
        {
            renderers.Add(Layer.Ground, new RegionRenderer(this, Layer.Ground));
        }
    }
}