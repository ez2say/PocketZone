using System.Collections.Generic;
using UnityEngine;

namespace PocketZone
{
    public class ObjectPool : MonoBehaviour
    {
        private Dictionary<GameObject, Queue<GameObject>> poolDictionary = new();

        public void AddPool(GameObject prefab, int size)
        {
            if (!poolDictionary.ContainsKey(prefab))
            {
                Queue<GameObject> objectPool = new();
                for (int i = 0; i < size; i++)
                {
                    GameObject obj = Instantiate(prefab);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }
                poolDictionary[prefab] = objectPool;
            }
            else
            {
                Queue<GameObject> objectPool = poolDictionary[prefab];
                for (int i = 0; i < size; i++)
                {
                    GameObject obj = Instantiate(prefab);
                    obj.SetActive(false);
                    objectPool.Enqueue(obj);
                }
            }
        }

        public GameObject GetObject(GameObject prefab)
        {
            Queue<GameObject> objectPool = poolDictionary[prefab];
            if (objectPool.Count > 0)
            {
                GameObject obj = objectPool.Dequeue();
                obj.SetActive(true);
                return obj;
            }
            else
            {
                Debug.LogWarning($"Пул для {prefab.name} пуст");
                GameObject newObj = Instantiate(prefab);
                newObj.SetActive(true);
                objectPool.Enqueue(newObj);
                return newObj;
            }
        }

        public void ReturnObjectToPool(GameObject obj)
        {
            foreach (var kvp in poolDictionary)
            {
                if (kvp.Value.Contains(obj))
                {
                    kvp.Value.Enqueue(obj);
                    obj.SetActive(false);
                    Debug.Log($"Объект {obj.name} вернулся в пул.");
                    return;
                }
            }
        }

        public bool ContainsObject(GameObject obj)
        {
            foreach (var queue in poolDictionary.Values)
            {
                foreach (var pooledObj in queue)
                {
                    if (pooledObj == obj)
                        return true;
                }
            }
            return false;
        }
    }
}