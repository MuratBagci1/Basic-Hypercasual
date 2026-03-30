using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;

public class Muzzle : MonoBehaviour, IStackable, IPooledObject, ISweepable
{
    public bool isStacked;
    public bool canBeCollected;
    public int stackOrder;


    private StackController stack;

    [Header("Referanslar")]
    [SerializeField] private ColorScriptable colorScriptable;
    [SerializeField] private TextMeshPro valueText;
    [SerializeField] private int value;

    private int colorValue;

    [Header("Wobble Deðerleri")]
    [SerializeField] private float wobbleDuration;
    [SerializeField] private float leftRightWobbleSpeed;
    [SerializeField] private float wobbleScale;
    [SerializeField] private float minLerpValue;
    [SerializeField] private float maxLerpValue;

    private float lerpTime;

    private PoolManager poolManager;
    private MuzzleManager muzzleManager;

    private MeshRenderer meshRenderer;
    private BoxCollider collider;
    private Rigidbody rb;

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

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<BoxCollider>();
        rb = GetComponent<Rigidbody>();

        canBeCollected = true;

        ActionManager.OnStackAwaken += GetStack;
    }

    private void Start()
    {
        valueText.text = value.ToString();

        colorValue = (int)MathF.Log(value, 2);

        meshRenderer.material.color = colorScriptable.GetColor(colorValue);

        ActionManager.OnMergeWobble += Wobble;

        poolManager = PoolManager.instance;
        muzzleManager = MuzzleManager.instance;
    }

    private void Update()
    {
        if (isStacked)
        {
            lerpTime = Mathf.Clamp(1 - leftRightWobbleSpeed * stackOrder * Time.deltaTime, minLerpValue, maxLerpValue);

            float newX = Mathf.Lerp(transform.position.x, stack.transform.position.x, lerpTime);

            transform.position = new Vector3(newX, stack.transform.position.y, stack.transform.position.z + stackOrder);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isStacked)
        {
            other.gameObject.GetComponent<IDamageable>()?.OnBlockCrashed(BlockCollided);
        }
    }

    private void BlockCollided()
    {
        StartCoroutine(BLockFall());
        ActionManager.OnBlockCollided?.Invoke(this);
    }

    IEnumerator BLockFall()
    {
        meshRenderer.material.color = Color.gray;

        collider.isTrigger = false;
        rb.useGravity = true;

        yield return new WaitForSeconds(0.5f);

        rb.velocity = Vector3.zero;

        collider.isTrigger = true;
        rb.useGravity = false;
    }

    public void OnInteractEnter(Action<Muzzle> callback)
    {
        callback.Invoke(this);
    }

    public void OnObjectInstantiated()
    {
        //Çalýþmamasý gerekiyor
        throw new Exception("Block Instantiate edildi");
    }

    public void OnObjectReturnedToPool()
    {
        DOTween.Kill(transform);

        transform.localScale = Vector3.one;

        canBeCollected = false;
    }

    public void OnObjectGetFromPool()
    {
        canBeCollected = true;
        isStacked = false;

        value = muzzleManager.RandomBlockValue();
        ChangeValueAndText(value);
    }

    public void ChangeValueAndText(int newValue)
    {
        value = newValue;

        colorValue = (int)MathF.Log(value, 2);

        valueText.text = value.ToString();

        meshRenderer.material.color = colorScriptable.GetColor(colorValue);
    }

    private void Wobble()
    {
        if (isStacked)
        {
            transform.DOScale(wobbleScale, wobbleDuration)
                .OnComplete(() => transform.DOScale(1f, 0.2f));
        }
    }

    public void MuzzleGotStacked()
    {
        transform.position = new Vector3(stack.transform.position.x, stack.transform.position.y, stack.transform.position.z + stackOrder);
    }

    private void GetStack(StackController stack)
    {
        this.stack = stack;
    }

    public void OnSweeped()
    {
        MuzzleReturnPool();
    }

    public void MuzzleReturnPool()
    {
        muzzleManager.CheckPossiblePositions();

        poolManager.ReturnObjectPool(PoolType.Muzzle, this.gameObject);
    }

    private void OnDestroy()
    {
        ActionManager.OnMergeWobble -= Wobble;
        ActionManager.OnStackAwaken -= GetStack;
    }
}