using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.Helpers;
using StandWorld.Visuals;
using UnityEngine;
using System;
using System.Collections.Generic;
using StandWorld.Game;

namespace StandWorld.World
{
    public class LayerGrid
    {
        public Vector2Int size
        {
            get { return rect.size; }
        }
        
        public RectI rect { get; protected set; }
        
        public LayerBucketGrid[] buckets { get; protected set; }
        
        public Type renderer { get; protected set; }
        
        public Layer layer { get; protected set; }

        private int _bucketSizeX;
        private int _bucketSizeY;
        private int _bucketCount;

        public LayerGrid(Vector2Int size, Layer layer)
        {
            this.layer = layer;
            rect = new RectI(new Vector2Int(0, 0), size);
            renderer = typeof(BucketRenderer);
            _bucketSizeX = Mathf.CeilToInt(this.size.x / (float)Settings.BUCKET_SIZE);
            _bucketSizeY = Mathf.CeilToInt(this.size.y / (float)Settings.BUCKET_SIZE);
            _bucketCount = _bucketSizeY * _bucketSizeX;
        }
        
        public void AddTilable(Tilable tilable)
        {
            GetBucketAt(tilable.position).AddTilable(tilable);
        }

        public void GenerateBuckets()
        {
            buckets = new LayerBucketGrid[_bucketCount];
            for (int x = 0; x < size.x; x += Settings.BUCKET_SIZE)
            {
                for (int y = 0; y < size.y; y += Settings.BUCKET_SIZE)
                {
                    RectI bucketRect = new RectI(new Vector2Int(x,y), Settings.BUCKET_SIZE, Settings.BUCKET_SIZE);
                    bucketRect.Clip(rect);
                    int bucketID =  (x / Settings.BUCKET_SIZE) + (y / Settings.BUCKET_SIZE) * _bucketSizeX;
                    buckets[bucketID] = new LayerBucketGrid(bucketID, bucketRect, layer, renderer);
                }
            }
        } 
        
        public LayerBucketGrid GetBucketAt(Vector2Int position)
        {
            int bucketID = (position.x / Settings.BUCKET_SIZE) + (position.y / Settings.BUCKET_SIZE) * _bucketSizeX;

            if (bucketID >= 0 && bucketID < buckets.Length)
            {
                return buckets[bucketID];
            }

            return null;
        }   
        
        public List<Tilable> GetTilables() {
            List<Tilable> r = new List<Tilable>();
            foreach (LayerBucketGrid bucket in this.buckets) {
                foreach (Tilable t in bucket.tilables) {
                    if (t != null) 
                        r.Add(t);
                }
            }
            return r;
        }

        public Tilable GetTilableAt(Vector2Int position)
        {
            LayerBucketGrid bucket = GetBucketAt(position);

            if (bucket != null)
            {
                return bucket.GetTilableAt(position);
            }

            return null;
        }

        public void CheckMatriceUpdates()
        {
            foreach (LayerBucketGrid bucket in buckets)
            {
                if (bucket.IsVisible())
                {
                    bucket.CheckMatriceUpdates();
                }
            }
        }

        public void BuildStaticMeshes()
        {
            if (renderer == null)
            {
                return;
            }
            
            foreach (LayerBucketGrid bucket in buckets)
            {
                bucket.BuildStaticMeshes();
            }
        }

        public void DrawBuckets()
        {
            if (renderer == null)
            {
                foreach (LayerBucketGrid bucket in buckets)
                {
                    if (bucket.IsVisible())
                    {
                        bucket.DrawInstanced();
                    }
                }
                return;
            }
            
            foreach (LayerBucketGrid bucket in buckets)
            {
                if (bucket.IsVisible())
                {
                    bucket.DrawStatics();
                    bucket.DrawInstanced();
                }
                
            }
        }

    }
}

