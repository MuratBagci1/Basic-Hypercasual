using System.Collections;
using UnityEngine;

public class SpinBarrel : MonoBehaviour
{
    private bool isRotating;
    [SerializeField] private float speed;
    private Coroutine spinCoroutine;

    private void Start()
    {
        ActionManager.OnTapToPlayPressed += StartSpinning;
        ActionManager.OnStoppedWalking += StopSpinning;
    }
    private void StartSpinning()
    {
        isRotating = true;
        spinCoroutine = StartCoroutine(Spin());
    }

    private void StopSpinning()
    {
        StopCoroutine(spinCoroutine);
    }

    private IEnumerator Spin()
    {
        while (isRotating)
        {
            transform.Rotate(Vector3.up * speed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnDestroy()
    {
        ActionManager.OnTapToPlayPressed -= StartSpinning;
        ActionManager.OnStoppedWalking -= StopSpinning;
    }
}