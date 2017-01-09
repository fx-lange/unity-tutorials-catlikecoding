using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    PooledObject prefab;

    public static ObjectPool GetPool(PooledObject prefab)
    {
        GameObject obj;
        ObjectPool pool;
        string poolname = prefab.name + "_Pool";
        if (Application.isEditor)
        {
            obj = GameObject.Find(poolname);
            if (obj)
            {
                pool = obj.GetComponent<ObjectPool>();
                if (pool)
                {
                    return pool;
                }
            }
        }
        obj = new GameObject(poolname);
        pool = obj.AddComponent<ObjectPool>();
        pool.prefab = prefab;
        return pool;
    }

    List<PooledObject> availableObjects = new List<PooledObject>();

    public PooledObject GetObject()
    {
        PooledObject obj;
        int lastAvailableIndex = availableObjects.Count - 1;
        if(lastAvailableIndex >= 0)
        {
            obj = availableObjects[lastAvailableIndex];
            availableObjects.RemoveAt(lastAvailableIndex);
            obj.gameObject.SetActive(true);
        }else
        {
            obj = Instantiate<PooledObject>(prefab);
            obj.transform.SetParent(transform);
            obj.Pool = this;
        }
        
        return obj;
    }

    public void AddObject(PooledObject obj)
    {
        obj.gameObject.SetActive(false);
        availableObjects.Add(obj);
    }
}
