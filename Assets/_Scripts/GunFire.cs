using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq;

public class GunFire : MonoBehaviour
{
    [SerializeField] private int firePower;
    [SerializeField] private int fireMultiplier;
    [SerializeField] private float fireCooldown;
    [SerializeField] private float timeBetweenBullets;

    [SerializeField] private Transform muzzleT;

    private bool isFiring;
    private Coroutine fireCR;


    private void Start()
    {
        ActionManager.OnTapToPlayPressed +=  StartCR;

        ActionManager.OnStoppedWalking += StopCR;

        ActionManager.OnMerge += ChangeFirePower;
    }

    private void StartCR()
    {
        isFiring = true;

        fireCR = StartCoroutine(StartFire()); 
    }

    private void StopCR()
    {
        StopCoroutine(fireCR);
        isFiring= false;
    }

    private IEnumerator StartFire()
    {
        while (isFiring)
        {
            StartCoroutine(Fire(firePower));

            yield return new WaitForSeconds(fireCooldown);
        }
    }

    private IEnumerator Fire(int bulletCount)
    {
        int spawnCount = (bulletCount % fireMultiplier == 0) ? fireMultiplier : bulletCount % fireMultiplier;

        for (int i = 0; i < spawnCount; i++)
        {
            PoolManager.instance.GetObjectFromPool(PoolType.Bullet, muzzleT.position, Quaternion.identity);

            yield return new WaitForSeconds(timeBetweenBullets);
        }
    }

    private void ChangeFirePower(List<Muzzle> stackedBlocks)
    {
        firePower = stackedBlocks.Max(b => b.Value);

        if (firePower > fireMultiplier)
        {
            int newDamage = firePower / fireMultiplier;

            ActionManager.OnDamageChanged?.Invoke(newDamage);
        }
    }

    private void OnDestroy()
    {
        ActionManager.OnTapToPlayPressed -= StartCR;

        ActionManager.OnStoppedWalking -= StopCR;

        ActionManager.OnMerge -= ChangeFirePower;
    }
}