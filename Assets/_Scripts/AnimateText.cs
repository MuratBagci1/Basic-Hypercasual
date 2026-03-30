using DG.Tweening;
using TMPro;
using UnityEngine;

public static class AnimateText
{
    private static float newScale = 1.5f;
    private static float scaleDuration = 0.5f;

    public static void ScaleText(TextMeshPro text)
    {
        text.transform.DOScale(newScale, scaleDuration).OnComplete(() =>
        {
            text.transform.DOScale(Vector3.one, scaleDuration);
        });
    }    
}