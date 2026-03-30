using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
public class Bullet : MonoBehaviour, IPooledObject
{
    [SerializeField] private int damage;
    [SerializeField] private Vector3 moveForce;
    [SerializeField] private float xEdge;
    [SerializeField] private float yEdge;
    [SerializeField] private float bulletLifeTime;

    private Vector3 firstPos;
    private Coroutine lifeTimeCR;

    private PoolManager poolManager;

    private ParticleSystem particle;
    private Rigidbody rb;
    private SphereCollider collider;

    private void Awake()
    {
        collider = GetComponent<SphereCollider>();
        rb = GetComponent<Rigidbody>();
        particle = GetComponent<ParticleSystem>();

    }

    private void Start()
    {
        ActionManager.OnDamageChanged += ChangeDamage;

        poolManager = PoolManager.instance;
    }

    private void FixedUpdate()
    {
        rb.velocity = moveForce;
    }

    private void OnTriggerEnter(Collider other)
    {
        other.GetComponent<IDamageable>()?.OnDamaged(damage);

        poolManager.ReturnObjectPool(PoolType.Bullet, this.gameObject);
    }

    public void OnObjectGetFromPool()
    {
        lifeTimeCR = StartCoroutine(LifeTime());
        particle.Play();

        transform.position += firstPos;

        //rb.AddForce(moveForce);
    }

    public void OnObjectInstantiated()
    {
        lifeTimeCR = StartCoroutine(LifeTime());

        particle.Play();

        firstPos += RandomPosition();

        transform.position += firstPos;

        //rb.AddForce(moveForce);
    }

    public void OnObjectReturnedToPool()
    {
        StopCoroutine(lifeTimeCR);

        particle.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);

        gameObject.SetActive(false);

        //rb.velocity = Vector3.zero;
    }

    private Vector3 RandomPosition()
    {
        float xRnd = UnityEngine.Random.Range(-xEdge, xEdge);
        float yRnd = UnityEngine.Random.Range(-yEdge, yEdge);

        Vector3 randomPosition = new Vector3(xRnd, yRnd, 0);

        return randomPosition;
    }

    private void ChangeDamage(int newDamage)
    {
        damage = newDamage;
    }

    private IEnumerator LifeTime()
    {
        yield return new WaitForSeconds(bulletLifeTime);

        poolManager.ReturnObjectPool(PoolType.Bullet, this.gameObject);
    }

    private void OnDestroy()
    {
        ActionManager.OnDamageChanged -= ChangeDamage;
    }
}