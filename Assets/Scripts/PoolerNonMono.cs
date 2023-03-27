using System.Collections.Generic;
using UnityEngine;
using Zenject;
using System;
using System.Reflection;


[System.Serializable]
public class PoolNonMono
{
    public TypeTag type;
    public GameObject prefab;
    public int size;

    [Serializable]
    public class TypeTag
    {
        public string _tag;
    }
}
public class PoolerNonMono : IInitializable
{
    private readonly List<PoolNonMono> pools;
    private readonly DiContainer instantiator;
    public PoolerNonMono(List<PoolNonMono> pool, DiContainer container)
    { 
        this.pools = pool; 
        this.instantiator = container;
    }

    private Dictionary<string, Queue<IPoolerNativ>> poolDictionary;

    public void Initialize()
    {
        poolDictionary = new Dictionary<string, Queue<IPoolerNativ>>();
        foreach (PoolNonMono pool in pools)
        {
            Queue<IPoolerNativ> objectPool = new Queue<IPoolerNativ>();
            for (int i = 0; i < pool.size; i++)
            {
                var prefab = GameObject.Instantiate(pool.prefab);
                var type = GetTypeNameFromString(pool.type._tag);
                IPoolerNativ moveable = (IPoolerNativ)instantiator.Instantiate(type);
                moveable.SetTransform(prefab.transform);
                moveable.Disable();
                objectPool.Enqueue(moveable);
            }

            poolDictionary.Add(pool.type._tag, objectPool);
        }
    }

    private Type GetTypeNameFromString(string typeName) => Type.GetType(typeName);

    public IPoolerNativ SpawnFromPool<T>(Vector3 position, Quaternion rotation) where T: IPoolerNativ
    {
        Type type = typeof(T);
        string typeName = type.GetTypeInfo().FullName;
        if (!poolDictionary.ContainsKey(typeName))
            throw new InvalidOperationException($"{typeName} is not define in Pooler");

        IPoolerNativ moveable = poolDictionary[typeName].Dequeue();
        moveable.Enable();
        moveable.SetStartPosition(position);
        moveable.SetStartRotation(rotation);

        poolDictionary[typeName].Enqueue(moveable);

        return moveable;
    }
}
