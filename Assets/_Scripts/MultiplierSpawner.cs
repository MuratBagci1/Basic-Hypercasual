using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiplierSpawner : MonoBehaviour
{
    [SerializeField] private GameObject multiplierPrefab;
    void Start()
    {
        InstantiateMultipliers();
    }

    private void InstantiateMultipliers()
    {
        Vector3 pos;
        for (int i = 1; i <= 10; i++) 
        {
            pos = new Vector3(0, 2.5f, transform.position.z + (i * 3));
            MultiplierController mlt = multiplierPrefab.GetComponent<MultiplierController>();
            mlt.PreInitialize(i);
            Instantiate(multiplierPrefab, pos, Quaternion.identity, gameObject.transform);            
        }
    }
}
