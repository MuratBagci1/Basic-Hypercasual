using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class StackController : MonoBehaviour
{
    private List<Muzzle> stackedBlocks;
    [SerializeField] private Transform blockParent;
    [SerializeField] private ColliderPositioner stackCollider;

    private void Awake()
    {
        stackedBlocks = new List<Muzzle>();
    }

    private void Start()
    {
        ActionManager.OnStackAwaken?.Invoke(this);
        ActionManager.OnBlockRemoved += RemoveFromStack;
        ActionManager.OnBlockCollided += RemoveFromStack;
        Merger.Initialize();
    }

    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<IStackable>()?.OnInteractEnter(StackBlock);
    }

    private void StackBlock(Muzzle muzzle)
    {
        if (!muzzle.isStacked && muzzle.canBeCollected)
        {
            muzzle.isStacked = true;
            muzzle.canBeCollected = false;

            stackedBlocks.Add(muzzle);

            stackCollider.SetStackCollider(1);
            stackCollider.SetMuzzlePosition(1);

            ActionManager.OnMerge(stackedBlocks);

            FindPosition();

            muzzle.MuzzleGotStacked();

            muzzle.transform.SetParent(transform, false);
        }
    }

    private void RemoveFromStack(Muzzle muzzle)
    {
        muzzle.isStacked = false;
        
        stackedBlocks.Remove(muzzle);

        stackCollider.SetStackCollider(-1);
        stackCollider.SetMuzzlePosition(-1);

        muzzle.transform.SetParent(blockParent, true);
    }

    private void FindPosition()
    {
        stackedBlocks = stackedBlocks.OrderByDescending(block => block.Value).ToList();

        foreach (Muzzle block in stackedBlocks)
        {
            block.stackOrder = stackedBlocks.IndexOf(block);
        }
    }

    private void OnDestroy()
    {
        ActionManager.OnBlockRemoved -= RemoveFromStack;
        ActionManager.OnBlockCollided -= RemoveFromStack;
    }
}