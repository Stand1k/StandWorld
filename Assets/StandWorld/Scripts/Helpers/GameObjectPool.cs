using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StandWorld.Helpers
{
    public class GameObjectPool
    {
        public Queue<GameObject> queue = new Queue<GameObject>();

        public GameObject GetFromPool()
        {
            GameObject go = queue.Dequeue();
            go.SetActive(true);
            return go;
        }

        public void AddFromClone(GameObject go,Transform parent, int qty)
        {
            for (int i = 0; i < qty; i++)
            {
                AddTolPool(GameObject.Instantiate(go, parent));
            }
            Object.Destroy(go);
        }

        public void AddTolPool(GameObject go)
        {
            go.SetActive(false);
            queue.Enqueue(go);
        }
        
    }
}
