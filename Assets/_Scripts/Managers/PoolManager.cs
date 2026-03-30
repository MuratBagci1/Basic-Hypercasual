using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public List<PooledObjectInfo> objectPools = new List<PooledObjectInfo>();

    GameObject emptyHolder;

    public static PoolManager instance;
    private void Awake()
    {
        instance = this;
        emptyHolder = new GameObject("EmptyHolder");
        SetUpEmpties();
    }

    private void SetUpEmpties()
    {
        for (int i = 0; i < objectPools.Count; i++)
        {
            PoolType type = objectPools[i].type;

            Transform poolParent = new GameObject(type.ToString()).transform;

            poolParent.parent = emptyHolder.transform;

            objectPools[i].poolParent = poolParent;
        }
    }

    public GameObject GetObjectFromPool(PoolType poolType, Vector3 spawnPosition, Quaternion spawnRotation)
    {
        PooledObjectInfo pool = objectPools.Find(p => p.type == poolType);

        if (pool == null)
        {
            Debug.Log($"Pool doesnt contain gameovject of type {poolType}");
        }

        GameObject spawnableObj = pool.inactiveObjects.FirstOrDefault();

        if (spawnableObj == null)
        {
            spawnableObj = SpawnObject(spawnPosition, spawnRotation, pool);
        }
        else
        {
            spawnableObj.transform.position = spawnPosition;
            spawnableObj.transform.rotation = spawnRotation;
            pool.inactiveObjects.Remove(spawnableObj);
        }

        spawnableObj.SetActive(true);

        spawnableObj.GetComponent<IPooledObject>()?.OnObjectGetFromPool();

        return spawnableObj;
    }

    private static GameObject SpawnObject(Vector3 spawnPosition, Quaternion spawnRotation, PooledObjectInfo pool)
    {
        GameObject spawnableObj = Instantiate(pool.prefab, spawnPosition, spawnRotation);

        spawnableObj.transform.parent = pool.poolParent;

        spawnableObj.GetComponent<IPooledObject>()?.OnObjectInstantiated();

        spawnableObj.name = pool.type.ToString();

        return spawnableObj;
    }

    public void ReturnObjectPool(PoolType type, GameObject obj)
    {
        PooledObjectInfo pool = objectPools.Find(p => p.type == type);

        obj.SetActive(false);

        //obj.transform.parent = pool.poolParent;

        obj.GetComponent<IPooledObject>()?.OnObjectReturnedToPool();

        pool.inactiveObjects.Add(obj);
    }
}

[Serializable]
public class PooledObjectInfo
{
    public PoolType type;
    public GameObject prefab;
    public Transform poolParent;
    public List<GameObject> inactiveObjects;

    public PooledObjectInfo(Transform poolParent, PoolType type)
    {
        this.poolParent = poolParent;
        this.type = type;
        this.inactiveObjects = new List<GameObject>();
    }
}

[Serializable]
public enum PoolType
{
    None,
    Muzzle,
    Obstacle,
    MovingObstacle,
    Particle,
    Fracture,
    Bullet
}

public class Parasdawdwa : MonoBehaviour, IPooledObject
{
    public void OnObjectGetFromPool()
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();
        if (particleSystem != null)
        {
            particleSystem.Play();
            //ReturnToPoolAfterDuration(spawnableObj, particleSystem.main.duration + particleSystem.main.startLifetime.constantMax);
        }
    }

    public void OnObjectInstantiated()
    {
        throw new NotImplementedException();
    }

    public void OnObjectReturnedToPool()
    {
        throw new NotImplementedException();
    }
}