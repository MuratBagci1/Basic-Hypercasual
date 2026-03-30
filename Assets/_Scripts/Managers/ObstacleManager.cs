using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleManager : MonoBehaviour
{
    [SerializeField] private float delay;
    [SerializeField] private List<ObstacleController> firstOrder;
    [SerializeField] private List<ObstacleController> secondOrder;
    [SerializeField] private List<int> values;
    [SerializeField] private List<int> tempRnd;
    private int seed;

    private PoolManager poolManager;

    public static ObstacleManager instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        tempRnd = values.ToList();

        poolManager = PoolManager.instance;
    }

    public void ReturnObstacleToPool(PoolType type, GameObject obs)
    {
        poolManager.ReturnObjectPool(type, obs.gameObject);
        StartCoroutine(TakeFromPool(type, obs.transform.position));
    }

    private IEnumerator TakeFromPool(PoolType type, Vector3 position)
    {
        yield return new WaitForSeconds(delay);
        poolManager.GetObjectFromPool(type, position, Quaternion.identity);
    }

    public int GenerateRandom()
    {
        int result = tempRnd[0];
        tempRnd.RemoveAt(0);

        if (tempRnd.Count == 0)
        {
            ReArrangeList();
        }

        return result;
    }

    private void ReArrangeList()
    {
        tempRnd = values.ToList();
        tempRnd = tempRnd.OrderBy(x => Guid.NewGuid()).ToList();
    }
}