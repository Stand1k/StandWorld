using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using StandWorld.Definitions;
using StandWorld.Entities;
using StandWorld.Game;
using StandWorld.Helpers;
using StandWorld.Visuals;
using UnityEngine;
using Random = UnityEngine.Random;

namespace StandWorld.World
{
    public class BucketProperty
    {
        public float vegetalNutriments;
        public float nutriments;

        public BucketProperty()
        {
            vegetalNutriments = 0f;
            nutriments = 0f;
        }
    }

    /// <summary>
    /// Регіон нашої сітки 
    /// </summary>
    public class LayerBucketGrid
    {
        public BucketProperty properties { get; protected set; }
        
        //Розмір цього регіона
        public RectI rect { get; protected set; }
        
        public Tilable[] tilables { get; protected set; }
        
        public int uId { get; protected set; }
        
        public bool rebuildMatrices;

        public Dictionary<TilableType, HashSet<Tilable>> tilablesByType { get; protected set; }

        //Словник(Dictionary) tilables matrices індексований по GraphicInstance.uId
        public Dictionary<int, List<Matrix4x4>> tilablesMatrices { get; protected set; }
        private Dictionary<int, Matrix4x4[]> tilablesMatricesArr;
        
        public Layer layer { get; protected set; }
        
        private BucketRenderer _staticRenderer;

        private bool _visible;

        public LayerBucketGrid(int uId, RectI rect, Layer layer, Type renderer) 
        {
            this.uId = uId;
            this.rect = rect;
            this.layer = layer;
            tilables = new Tilable[rect.width * rect.height];
            tilablesByType = new Dictionary<TilableType, HashSet<Tilable>>();
            tilablesMatrices = new Dictionary<int, List<Matrix4x4>>();
            tilablesMatricesArr = new Dictionary<int, Matrix4x4[]>();
            properties = new BucketProperty();
            
            if (renderer != null)
            {
                _staticRenderer = (BucketRenderer)Activator.CreateInstance(renderer, this, this.layer);
            }
        }

        public void SetVisible(bool visible)
        {
            _visible = visible;
        }

        /// <summary>
        /// Перевіряє чи попадає даний регіон під область видимості камери
        /// </summary>
        /// <returns>Повертає true коли регіон попав під видимість камери</returns>
        public bool CalcVisible()
        {
            _visible = (
                rect.min.x >= ToolBox.cameraController.viewRect.min.x - Map.BUCKET_SIZE &&
                rect.max.x <= ToolBox.cameraController.viewRect.max.x + Map.BUCKET_SIZE &&
                rect.min.y >= ToolBox.cameraController.viewRect.min.y - Map.BUCKET_SIZE &&
                rect.max.y <= ToolBox.cameraController.viewRect.max.y + Map.BUCKET_SIZE
            );

            return _visible;
        }
        
        public bool IsVisible()
        {
            return _visible;
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

            tilablesMatricesArr = new Dictionary<int, Matrix4x4[]>();
            foreach (KeyValuePair<int,List<Matrix4x4>> kv in tilablesMatrices)
            {
                tilablesMatricesArr.Add(kv.Key, kv.Value.ToArray());
            }
        }
        
        public void DrawStatics()
        {
            _staticRenderer.Draw();
        }

        public void CheckMatriceUpdates()
        {
            if (rebuildMatrices && IsVisible())
            {
                UpdateMatrices();
                rebuildMatrices = false;
            }
        }

        public void DrawInstanced()
        {
            foreach (KeyValuePair<int, Matrix4x4[]> kv in tilablesMatricesArr)
            {
               Graphics.DrawMeshInstanced(
                    GraphicInstance.instances[kv.Key].mesh,
                    0,
                    GraphicInstance.instances[kv.Key].material,
                    kv.Value
                    );
            }
        }

        public void BuildStaticMeshes()
        { 
            _staticRenderer.BuildMeshes();
        }
        
        /// <summary>
        /// Конвертує глобальні координати тайла в локальні для цього регіона
        /// </summary>
        /// <param name="globalPosition">Глобальна позиція на сітці</param>
        /// <returns>Повертає локальну позицію тайлу для цього регіона</returns>
        public Vector2Int GetLocalPosition(Vector2Int globalPosition)
        {
            return new Vector2Int(globalPosition.x - rect.min.x, globalPosition.y - rect.min.y);
        }

        public Tilable GetTilableAt(Vector2Int position)
        {
            Vector2Int localPosition = GetLocalPosition(position);
            if (localPosition.x >= 0 && 
                localPosition.y >= 0 &&
                localPosition.x < rect.width &&
                localPosition.y < rect.height)
            {
                return tilables[localPosition.x + localPosition.y * rect.width];
            }

            return null;
        }
        
        public void AddTilable(Tilable tilable)
        {
            Vector2Int localPosition = GetLocalPosition(tilable.position);
            tilables[localPosition.x + localPosition.y * rect.width] = tilable;
            tilable.SetBucket(this);
            //При додавані тайлу оновлюємо дані нашого TileProperty для Pathfinding 
            ToolBox.map[tilable.position].Update();

            if (tilable.def.type != TilableType.Undefined)
            {
                if (!tilablesByType.ContainsKey(tilable.def.type))
                {
                    tilablesByType.Add(tilable.def.type, new HashSet<Tilable>());
                }

                tilablesByType[tilable.def.type].Add(tilable);
            }

            if (tilable.def.type == TilableType.Grass)
            {
                if (tilable.def.nutriments > 0f)
                {
                    properties.vegetalNutriments += tilable.def.nutriments;
                    properties.nutriments += tilable.def.nutriments;
                }
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

            rebuildMatrices = true;
        }
        
        public void DelTilable(Tilable tilable) {
            Vector2Int localPosition = GetLocalPosition(tilable.position);
            
            if (tilable.def.type == TilableType.Grass)
            {
                if (tilable.def.nutriments > 0f)
                {
                    properties.vegetalNutriments -= tilable.def.nutriments;
                    properties.nutriments -= tilable.def.nutriments;
                }
            }
            
            tilables[localPosition.x + localPosition.y * rect.width] = null;
            ToolBox.map[tilable.position].Update();

            if (tilable.def.type != TilableType.Undefined) {
                tilablesByType[tilable.def.type].Remove(tilable);
                if (tilablesByType[tilable.def.type].Count == 0) {
                    tilablesByType.Remove(tilable.def.type);
                }
            }

            if (tilable.def.graphics.isInstanced)
            {
               rebuildMatrices = true;
            }
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