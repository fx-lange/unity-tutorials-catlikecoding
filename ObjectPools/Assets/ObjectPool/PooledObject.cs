using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    public ObjectPool Pool { get; set; }

    [System.NonSerialized]
    ObjectPool poolInstance;

    public void ReturnToPool()
    {
        if (Pool)
        {
            Pool.AddObject(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public T Create<T>() where T : PooledObject
    {
        if (!poolInstance)
        {
            poolInstance = ObjectPool.GetPool(this);
        }
        return (T)poolInstance.GetObject();
    }
}
