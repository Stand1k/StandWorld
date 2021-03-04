using System;
using System.Collections.Generic;
using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.Helpers;
using StandWorld.Visuals;
using UnityEngine;

namespace StandWorld.World
{
    public class LayerBucketGrid
    {
        public RectI rect { get; protected set; }
        
        public Tilable[] tilables { get; protected set; }
        
        public int uId { get; protected set; }

        public bool rebuildMatrices;
        
        public Dictionary<TilableType, HashSet<Tilable>> tilablesByType { get; protected set; }
        
        //Словник(Dictionary) tilables matrices індексований по GraphicInstance.uId
        public Dictionary<int, List<Matrix4x4>> tilablesMatrices { get; protected set; }
        
        public Layer layer { get; protected set; }
        
        private BucketRenderer _staticRenderer;


        public LayerBucketGrid(int uId, RectI rect, Layer layer, Type renderer) 
        {
            this.uId = uId;
            this.rect = rect;
            this.layer = layer;
            tilables = new Tilable[rect.size.x * rect.size.y];
            tilablesByType = new Dictionary<TilableType, HashSet<Tilable>>();
            tilablesMatrices = new Dictionary<int, List<Matrix4x4>>();
            if (renderer != null) {
                _staticRenderer = (BucketRenderer)Activator.CreateInstance(renderer, this, this.layer);
            }
        }
        
        public bool IsVisible()
        {
            return(
                rect.min.x >= ToolBox.cameraController.viewRect.min.x - Map.REGION_SIZE && 
                rect.max.x <= ToolBox.cameraController.viewRect.max.x + Map.REGION_SIZE && 
                rect.min.y >= ToolBox.cameraController.viewRect.min.y - Map.REGION_SIZE &&
                rect.max.y <= ToolBox.cameraController.viewRect.max.y + Map.REGION_SIZE
            );
        }

        public void UpdateMatrices()
        {
            tilablesMatrices = new Dictionary<int, List<Matrix4x4>>();
            foreach (Tilable tilable in tilables)
            {
                if (tilable != null && tilable.def.graphics.isInstanced)
                {
                    AddMatrice(tilable.mainGraphic.uId, tilable.GetMatrice(tilable.mainGraphic.uId));
                    if (tilable.addGraphics != null)
                    {
                        foreach (GraphicInstance graphicInstance in tilable.addGraphics.Values)
                        {
                            AddMatrice(graphicInstance.uId, tilable.GetMatrice(graphicInstance.uId));
                        }
                    }
                }
            }
        }
        
        public void DrawStatics()
        {
            _staticRenderer.Draw();
        }

        public void DrawInstanced()
        {
            if (rebuildMatrices && IsVisible())
            {
                UpdateMatrices();
                rebuildMatrices = false;
            }

            foreach (KeyValuePair<int,List<Matrix4x4>> kv in tilablesMatrices)
            {
                Graphics.DrawMeshInstanced(
                    MeshPool.GetPlaneMesh(GraphicInstance.instances[kv.Key].def.size),
                    0,
                    GraphicInstance.instances[kv.Key].material,
                    kv.Value.ToArray()
                    );
            }
        }

        public void BuildStaticMeshes()
        {
            _staticRenderer.BuildMeshes();
        }

        public Vector2Int GetLocalPosition(Vector2Int globalPosition)
        {
            return new Vector2Int(globalPosition.x - rect.min.x, globalPosition.y - rect.min.y);
        }

        public Tilable GetTilableAt(Vector2Int position)
        {
            Vector2Int localPosition = GetLocalPosition(position);
            if (localPosition.x >= 0 && 
                localPosition.y >= 0 &&
                localPosition.x < rect.size.x &&
                localPosition.y < rect.size.y)
            {
                return tilables[localPosition.x + localPosition.y * rect.size.y];
            }

            return null;
        }
        

        public void AddTilable(Tilable tilable)
        {
            Vector2Int localPosition = GetLocalPosition(tilable.position);
            tilables[localPosition.x + localPosition.y * rect.size.y] = tilable;
            tilable.SetBucket(this);

            if (tilable.def.type != TilableType.Undefined)
            {
                if (!tilablesByType.ContainsKey(tilable.def.type))
                {
                    tilablesByType.Add(tilable.def.type, new HashSet<Tilable>());
                }

                tilablesByType[tilable.def.type].Add(tilable);
            }
            
            if (tilable.def.graphics.isInstanced)
            {
                AddMatrice(tilable.mainGraphic.uId, tilable.GetMatrice(tilable.mainGraphic.uId));
                if (tilable.addGraphics != null)
                {
                    foreach (GraphicInstance graphicInstance in tilable.addGraphics.Values)
                    {
                        AddMatrice(graphicInstance.uId, tilable.GetMatrice(graphicInstance.uId));
                    }
                }
            }
        }
        
        
        public void DelTilable(Tilable tilable) {
            Vector2Int localPosition = GetLocalPosition(tilable.position);
            tilables[localPosition.x + localPosition.y * rect.size.y] = null;

            if (tilable.def.type != TilableType.Undefined) {
                tilablesByType[tilable.def.type].Remove(tilable);
                if (tilablesByType[tilable.def.type].Count == 0) {
                    tilablesByType.Remove(tilable.def.type);
                }
            }

            rebuildMatrices = true;
        }

        public void AddMatrice(int graphicID, Matrix4x4 matrice)
        {
            if (!tilablesMatrices.ContainsKey(graphicID))
            {
                tilablesMatrices.Add(graphicID, new List<Matrix4x4>());
            }
            tilablesMatrices[graphicID].Add(matrice);
        }
    }
    
    
}