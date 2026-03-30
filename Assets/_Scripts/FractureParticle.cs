using System.Collections;
using UnityEngine;

public class FractureParticle : MonoBehaviour
{
    [SerializeField] private float fadeSpeed;
    [SerializeField] private float fadeTime;
    [SerializeField] private float fadeDuration;
    [SerializeField] private float minFadeValue;
    private MeshRenderer meshRenderer;
    private Vector3 initialPos;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        initialPos = transform.localPosition;
    }

    private void OnEnable()
    {
        transform.localPosition = initialPos;

        meshRenderer.material = new Material(meshRenderer.material);
        Color newColor = meshRenderer.material.color;
        newColor.a = 1;
        meshRenderer.material.color = newColor;

        StartCR();
    }


    [ContextMenu("FadeAway")]
    private void StartCR()
    {
        StartCoroutine(FadeAway());
    }

    IEnumerator FadeAway()
    {
        fadeDuration = 1f;
        while (fadeDuration > minFadeValue)
        {
            fadeDuration -= fadeSpeed;

            Color newColor = meshRenderer.material.color;
            newColor.a = fadeDuration;
            meshRenderer.material.color = newColor;

            yield return new WaitForSeconds(fadeTime);
        }
    }
}