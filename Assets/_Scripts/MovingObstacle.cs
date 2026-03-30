using DG.Tweening;
using System;
using System.Collections;
using UnityEngine;

public class MovingObstacle : MonoBehaviour, IDamageable, ISweepable, IPooledObject
{
    [SerializeField] private bool isMoving;
    [SerializeField] private Transform target;
    [SerializeField] private float delay;

    [SerializeField] private float movePoint;
    [SerializeField] private float horizontalMoveSpeed;
    [SerializeField] private float verticalMoveSpeed;

    [SerializeField] private MovingObstacleType moveType;
    [SerializeField] public PoolType poolType;

    public IDamageable.DamageableType Type => IDamageable.DamageableType.Moving;

    public bool IsDead => false;

    private void Start()
    {
        ActionManager.OnTapToPlayPressed += StartMoveCR;
        ActionManager.OnStoppedWalking += StopMoveCR;
    }

    private void StartMoveCR()
    {
        StartCoroutine(DelayCR());
    }

    private IEnumerator DelayCR()
    {
        yield return new WaitForSeconds(delay);

        isMoving = true;

        if (moveType == MovingObstacleType.Vertical)
        {
            MoveVertical();
        }
        else
        {
            MoveHorizontal();
        }
    }

    private void StopMoveCR()
    {
        isMoving = false;
    }


    private void MoveVertical()
    {
        target.DORotate(new Vector3(0, 0, -90), verticalMoveSpeed)
                 .SetLoops(-1, LoopType.Yoyo)
                 .SetEase(Ease.InOutSine);
    }

    private void MoveHorizontal()
    {
        transform.DOMoveX(movePoint, horizontalMoveSpeed)
            .SetLoops(-1, LoopType.Yoyo)
            .SetEase(Ease.InOutSine)
            .WaitForCompletion();
    }

    public void OnBlockCrashed(Action callback)
    {
        callback.Invoke();
    }

    public void OnSweeped()
    {
        ObstacleManager.instance.ReturnObstacleToPool(poolType, this.gameObject);
    }

    public void OnObjectInstantiated()
    {
        if (PositionManager.instance.movingObstaclePositions.Count > 0)
        {
            transform.localPosition = PositionManager.instance.GiveMovingObstaclePosition();
        }
        else
        {
            PoolManager.instance.ReturnObjectPool(poolType, this.gameObject);
        }

        StartCoroutine(DelayCR());
    }

    public void OnObjectReturnedToPool()
    {
        StopMoveCR();
        transform.DOKill();
    }

    public void OnObjectGetFromPool()
    {
        if (PositionManager.instance.movingObstaclePositions.Count > 0)
        {
            transform.localPosition = PositionManager.instance.GiveMovingObstaclePosition();
        }
        else
        {
            PoolManager.instance.ReturnObjectPool(poolType, this.gameObject);
        }

        StartCoroutine(DelayCR());
    }

    private void OnDestroy()
    {
        ActionManager.OnTapToPlayPressed -= StartMoveCR;
        ActionManager.OnStoppedWalking -= StopMoveCR;
    }

    private enum MovingObstacleType
    {
        None,
        Vertical,
        Horizontal
    }
}