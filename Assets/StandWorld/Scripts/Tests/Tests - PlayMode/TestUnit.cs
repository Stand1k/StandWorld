using System.Collections;
using System.Collections.Generic;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;
using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.Helpers;
using StandWorld.Visuals;
using StandWorld.World;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests
{
    public class TestUnit
    {
        [Test]
        public void LayerBucketIsVisible_True_Test()
        {
            LayerBucketGrid layerBucketGrid = new LayerBucketGrid(123, new RectI(new Vector2Int(10,10), new Vector2Int(15,15)), Layer.Ground, typeof(BucketGroundRenderer));
            layerBucketGrid.SetVisible(true);

            Assert.True(layerBucketGrid.IsVisible());
        }
        
        [Test]
        public void LayerBucketIsVisible_False_Test()
        {
            LayerBucketGrid layerBucketGrid = new LayerBucketGrid(123, new RectI(new Vector2Int(10,10), new Vector2Int(15,15)), Layer.Ground, typeof(BucketGroundRenderer));
            layerBucketGrid.SetVisible(false);

            Assert.False(layerBucketGrid.IsVisible());
        }
        
        [Test]
        public void LayerBucketGetTilableAt_NULL_Test()
        {
            RectI rect = new RectI(new Vector2Int(0, 0), new Vector2Int(15,15));
            
            LayerBucketGrid layerBucketGrid = new LayerBucketGrid(123, rect, Layer.Ground, typeof(BucketGroundRenderer));

            for (int i = 0; i < rect.area; i++)
            {
                layerBucketGrid.tilables[i] = new Tilable();
            }

            Tilable tilable = layerBucketGrid.GetTilableAt(new Vector2Int(23, 52));

            Assert.Null(tilable);
        }
        
        [Test]
        public void LayerBucketGetTilableAt_NOT_NULL_Test()
        {
            RectI rect = new RectI(new Vector2Int(0, 0), new Vector2Int(15,15));
            
            LayerBucketGrid layerBucketGrid = new LayerBucketGrid(123, rect, Layer.Ground, typeof(BucketGroundRenderer));

            for (int i = 0; i < rect.area; i++)
            {
                layerBucketGrid.tilables[i] = new Tilable();
            }

            Tilable tilable = layerBucketGrid.GetTilableAt(new Vector2Int(1, 1));

            Assert.NotNull(tilable);
        }
    }
}
