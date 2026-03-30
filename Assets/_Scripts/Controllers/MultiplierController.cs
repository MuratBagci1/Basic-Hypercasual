using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using static ColorScriptable;
using static IDamageable;

public class MultiplierController : MonoBehaviour, IDamageable
{
    [SerializeField] private ColorScriptable multiplierScriptable;

    [SerializeField] private int multiplierValue;
    [SerializeField] private TextMeshPro valueText;
    [SerializeField] private TextMeshPro multiplierText;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int value;
    [SerializeField] private int valueMultiplier;
    private bool isDead;

    public bool IsDead
    {
        get 
        { 
            return isDead;
        } 
    }

    private MeshRenderer meshRenderer;
    private BoxCollider collider;

    public DamageableType Type => DamageableType.Multiplier;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        collider = GetComponent<BoxCollider>();
    }

    private void Start()
    {
        meshRenderer.material.color = multiplierScriptable.GetColor(multiplierValue);
        //meshRenderer.material.color = ActionManager.GetColor(multiplierValue);
    }

    public void PreInitialize(int i)
    {
        multiplierValue = i;
        multiplierText.text = i.ToString() + "x";
        value = 100 * i * valueMultiplier;
        valueText.text = value.ToString();
    }

    public void OnDamaged(int damage)
    {
        value -= damage;
        if (!isDead)
        {
            ChangeValue(damage);
        }
    }

    private int ChangeValue(int damage)
    {
        value -= damage;
        if (value <= 0 && !isDead)
        {
            KillMultiplier();
            value = 0;
        }
        else
        {
            value = Mathf.Max(value, 0);
        }
        valueText.text = value.ToString();

        AnimateText.ScaleText(valueText);
        return value;
    }
    private void KillMultiplier()
    {
        transform.DOMoveY(-1.49f, moveSpeed);
        collider.enabled = false;
        ActionManager.OnMultiplierDied?.Invoke();
        isDead = true;
    }

    public void OnBlockCrashed(Action callback)
    {
        callback.Invoke();
    }
}