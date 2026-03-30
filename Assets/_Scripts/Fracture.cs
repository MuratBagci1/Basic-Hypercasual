using System.Collections;
using UnityEngine;

public class Fracture : MonoBehaviour
{
    [Tooltip("\"Fractured\" is the object that this will break into")]
    [SerializeField] private GameObject fractured;
    [SerializeField] private float fadeDuration;

    public void StartFracture()
    {
        GameObject obj = PoolManager.instance.GetObjectFromPool(PoolType.Fracture, transform.position, Quaternion.identity);
        StartCoroutine(ReturnFracture(obj));
    }

    IEnumerator ReturnFracture(GameObject ObjToFade)
    {
        yield return new WaitForSeconds(fadeDuration);
        PoolManager.instance.ReturnObjectPool(PoolType.Fracture,ObjToFade);
    }
}