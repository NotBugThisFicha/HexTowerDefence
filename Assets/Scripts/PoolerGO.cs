using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PoolerGO : IInitializable
{
    public PoolerGO(List<Pool> pools)
    {
        this.pools = pools;
    }

    [System.Serializable]
    public class Pool
    {
        public string _tag;
        public GameObject prefab;
        public int size;
    }

    private readonly List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;

    public void Initialize()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = GameObject.Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool._tag, objectPool);
        }
    }

    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        if (!poolDictionary.ContainsKey(tag))
            return null;

        GameObject objectToSpawn = poolDictionary[tag].Dequeue();
        objectToSpawn.SetActive(true);
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        poolDictionary[tag].Enqueue(objectToSpawn);

        return objectToSpawn;
    }
}
