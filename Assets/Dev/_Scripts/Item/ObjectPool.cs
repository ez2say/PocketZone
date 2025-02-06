using System.Collections.Generic;
using UnityEngine;

namespace PocketZone
{
    public class ObjectPool : MonoBehaviour
    {
        private Dictionary<GameObject, Queue<GameObject>> _pools = new Dictionary<GameObject, Queue<GameObject>>();

        public void AddPool(GameObject prefab, int size)
        {
            if (!_pools.ContainsKey(prefab))
            {
                var pool = new Queue<GameObject>();
                for (int i = 0; i < size; i++)
                {
                    GameObject obj = Instantiate(prefab);
                    obj.SetActive(false);
                    pool.Enqueue(obj);
                }
                _pools[prefab] = pool;
            }
        }

        public GameObject GetObject(GameObject prefab)
        {
            Queue<GameObject> poolQueue = _pools[prefab];
            
            if (poolQueue.Count > 0)
            {
                GameObject obj = poolQueue.Dequeue();
                obj.SetActive(true);
                Debug.Log("Вернули объект");
                return obj;
            }
            else
            {
                GameObject newObj = Instantiate(prefab);
                newObj.SetActive(true);
                poolQueue.Enqueue(newObj);
                Debug.Log("Вернули новый объект");
                return newObj;
            }
        }

        public void ReturnObjectToPool(GameObject obj)
        {
            foreach (var pool in _pools)
            {
                if (IsInstanceOfPrefab(obj, pool.Key))
                {
                    obj.SetActive(false);
                    pool.Value.Enqueue(obj);
                    return;
                }
            }
            Debug.LogWarning($"Объект {obj.name} Не часть пула");
        }

        private bool IsInstanceOfPrefab(GameObject instance, GameObject prefab)
        {
            return instance.name.StartsWith(prefab.name);
        }
    }
}