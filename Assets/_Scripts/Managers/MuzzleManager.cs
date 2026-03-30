using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MuzzleManager : MonoBehaviour
{
    [SerializeField] private GameObject blockParent;
    [SerializeField] private float placeDelay;

    private PoolManager poolManager;
    private PositionManager positionManager;

    public static MuzzleManager instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        poolManager = PoolManager.instance;
        positionManager = PositionManager.instance;
    }

    public int RandomBlockValue()
    {
        float rnd = UnityEngine.Random.Range(0,1f);
        if(rnd > .33f)
        {
            return 4;
        }
        else
        {
            return 2;
        }
    }

    public void CheckPossiblePositions()
    {
        if (positionManager.muzzlePositions.Count > 0)
        {
            Vector3 newPos = positionManager.GiveBlockPosition();

            StartCoroutine(PlaceMuzzle(newPos));
        }
    }

    private IEnumerator PlaceMuzzle(Vector3 newPos)
    {
        yield return new WaitForSeconds(placeDelay);

        poolManager.GetObjectFromPool(PoolType.Muzzle, newPos, Quaternion.identity);
    }
}