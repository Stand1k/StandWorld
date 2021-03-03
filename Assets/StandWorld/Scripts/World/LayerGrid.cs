using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.Helpers;
using StandWorld.Visuals;
using UnityEditor;
using UnityEditorInternal.Profiling.Memory.Experimental;
using UnityEngine;
using System.Collections.Generic;
using System;

namespace StandWorld.World
{
    public class LayerGrid
    {
        public Vector2Int size
        {
            get { return rect.size; }
        }
        
        public  RectI rect { get; protected set; }
        
        public LayerGridBucket[] buckets { get; protected set; }
        
        public Type renderer { get; protected set; }
        
        public Layer layer { get; protected set; }

        private int _bucketSizeX;
        private int _bucketCount;

        
        public LayerGrid(Vector2Int size, Layer layer)
        {
            this.layer = layer;
            rect = new RectI(new Vector2Int(0, 0), size);
            this.renderer = typeof(BucketRenderer);
            _bucketSizeX = Mathf.CeilToInt(size.x / Settings.BUCKET_SIZE);
            _bucketCount = Mathf.CeilToInt(size.x / Settings.BUCKET_SIZE) * _bucketSizeX;
            GenerateBuckets();
        }
        
     
        
        public void AddTilable(Tilable tilable)
        {
            GetBucketAt(tilable.position).AddTilable(tilable);
        }

        public void GenerateBuckets()
        {
            buckets = new LayerGridBucket[_bucketCount];
            for (int x = 0; x < size.x; x += Settings.BUCKET_SIZE)
            {
                for (int y = 0; y < size.y; y += Settings.BUCKET_SIZE)
                {
                    RectI bucketRect = new RectI(new Vector2Int(x,y), Settings.BUCKET_SIZE, Settings.BUCKET_SIZE);
                    bucketRect.Clip(rect);
                    int bucketID = (int) (x / Settings.BUCKET_SIZE) +
                                   (int) (y / Settings.BUCKET_SIZE) * _bucketSizeX;
                    buckets[bucketID] = new LayerGridBucket(bucketID, bucketRect, layer, renderer);
                }
            }
        } 
        
        public LayerGridBucket GetBucketAt(Vector2Int position)
        {
            int bucketID = (int) (position.x / Settings.BUCKET_SIZE) +
                           (int) (position.y / Settings.BUCKET_SIZE) * _bucketSizeX;

            if (bucketID >= 0 && bucketID < buckets.Length)
            {
                return buckets[bucketID];
            }

            return null;
        }

        public Tilable GetTilableAt(Vector2Int position)
        {
            LayerGridBucket bucket = GetBucketAt(position);

            if (bucket != null)
            {
                return GetBucketAt(position).GetTilableAt(position);
            }

            return null;
        }

        public void BuildStaticMeshes()
        {
            foreach (LayerGridBucket bucket in buckets)
            {
                bucket.BuildStaticMeshes();
            }
        }

        public void DrawBuckets()
        {
            if (renderer == null)
            {
                foreach (LayerGridBucket bucket in buckets)
                {
                    if (!bucket.IsVisible())
                    {
                        continue;
                    }
                    bucket.DrawInstanced();
                }
                return;
            }
            
            foreach (LayerGridBucket bucket in buckets)
            {
                if (!bucket.IsVisible())
                {
                    continue;
                }
                
                bucket.DrawStatics();
                bucket.DrawInstanced();
            }
        }

    }

    public class GroundGrid : LayerGrid
    {
        public GroundGrid(Vector2Int size) : base(size, Layer.Ground)
        {
            renderer = typeof(BucketGroundRenderer);
            GenerateBuckets();
        }
    }
}

