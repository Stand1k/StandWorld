using System;
using System.Collections.Generic;
using StandWorld.Entities;
using StandWorld.Helpers;
using UnityEngine;

namespace StandWorld.Controllers
{
    public class StackableLabelController : MonoBehaviour
    {
        public static GameObjectPool goPool = new GameObjectPool();

        public List<GameObject> actives = new List<GameObject>();

        private void Awake()
        {
            if (goPool.go_Queue.Count == 0)
            {
                GameObject go = new GameObject("Label GameObject");
                go.transform.SetParent(transform);
                go.AddComponent<LabelComponent>();
                
                //Додаємо в пул n клонів GameObject go
                goPool.AddFromClone(go,transform,100);
            }
        }

        public void AddLabel(Stackable stackable)
        {
            GameObject go = goPool.GetFromPool();
            go.GetComponent<LabelComponent>().SetStackable(stackable);
        }
    }
}
