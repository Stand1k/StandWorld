using System.Collections.Generic;
using StandWorld.Definitions;
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

        public MapRegion(int id, RectI regionRect, Map map)
        {
            this.regionRect = regionRect;
            this.id = id;
            this.map = map;
            this.renderers = new Dictionary<Layer, RegionRenderer>();
            AddRenderers();
        }

        public void Draw()
        {
            foreach (RegionRenderer regionRenderer in renderers.Values)
            {
                regionRenderer.Draw();
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
