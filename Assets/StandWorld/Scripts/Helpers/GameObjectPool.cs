using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StandWorld.Helpers
{
    public class GameObjectPool
    {
        public Queue<GameObject> go_Queue = new Queue<GameObject>();

        public GameObject GetFromPool()
        {
            GameObject go = go_Queue.Dequeue();
            go.SetActive(true);
            return go;
        }

        public void AddFromClone(GameObject go,Transform parent, int qty)
        {
            for (int i = 0; i < qty; i++)
            {
                AddToPool(GameObject.Instantiate(go, parent));
            }
            Object.Destroy(go);
        }

        public void AddToPool(GameObject go)
        {
            go.SetActive(false);
            go_Queue.Enqueue(go);
        }
        
    }
}
