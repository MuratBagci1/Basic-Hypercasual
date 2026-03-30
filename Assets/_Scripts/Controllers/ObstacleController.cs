using System;
using System.Collections;
using TMPro;
using UnityEngine;
using static IDamageable;

public class ObstacleController : MonoBehaviour, IDamageable, ISweepable, IPooledObject
{
    [SerializeField] private int value;
    [SerializeField] private int order;
    [SerializeField] public TextMeshPro valueText;
    [SerializeField] private float newScale;
    [SerializeField] private float scaleDuration;
    [SerializeField] private Vector3 pushBack;

    [SerializeField] public PoolType poolType;

    private ObstacleManager obstacleManager;
    private PositionManager positionManager;

    public MeshRenderer meshRenderer;
    public BoxCollider collider;
    public Rigidbody rb;
    private Fracture fractureScript;
    public bool isDead;

    public bool IsDead
    {
        get
        {
            return isDead;
        }
    }

    public DamageableType Type => DamageableType.Obstacle;

    public int Value
    {
        set
        {
            this.value = value;
        }
        get
        {
            return value;
        }
    }

    public TextMeshPro ValueText
    {
        set
        {
            valueText = value;
        }
        get
        {
            return valueText;
        }
    }

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();
        fractureScript = GetComponent<Fracture>();

        valueText.text = Value.ToString();
    }

    private void Start()
    {
        obstacleManager = ObstacleManager.instance;
        positionManager = PositionManager.instance;
    }

    public void OnDamaged(int damage)
    {
        if (!isDead)
        {
            PushBack();
            ChangeValue(damage);
        }
    }

    public void PushBack()
    {
        transform.position += pushBack;
    }

    private int ChangeValue(int damage)
    {
        value = Mathf.Max(0, value - damage);

        if (value == 0)
        {
            KillObstacle();
        }

        valueText.text = value.ToString();
        AnimateText.ScaleText(valueText);
        return value;
    }

    private void KillObstacle()
    {
        isDead = true;

        fractureScript.StartFracture();

        obstacleManager.ReturnObstacleToPool(poolType, this.gameObject);
    }

    public void OnBlockCrashed(Action callback)
    {
        callback.Invoke();
    }

    public void OnSweeped()
    {
        obstacleManager.ReturnObstacleToPool(poolType, this.gameObject);
    }

    public void OnObjectInstantiated()
    {
        float newX = positionManager.GiveObstaclePosition(order);

        transform.position = new Vector3(transform.localPosition.x, transform.localPosition.y, newX);

        value = obstacleManager.GenerateRandom() * order;
        ValueText.text = value.ToString();

        isDead = false;
    }

    public void OnObjectReturnedToPool()
    {
        order += 2;
    }

    public void OnObjectGetFromPool()
    {
        if(order < positionManager.obstaclePositions.Count)
        {
            float newX = positionManager.GiveObstaclePosition(order);
            transform.position = new Vector3(transform.localPosition.x, transform.localPosition.y, newX);
        }
        else
        {
            obstacleManager.ReturnObstacleToPool(poolType, this.gameObject);
        }

        value = obstacleManager.GenerateRandom() * order;
        ValueText.text = value.ToString();

        isDead = false;
    }
}