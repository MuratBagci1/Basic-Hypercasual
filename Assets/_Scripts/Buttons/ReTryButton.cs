using UnityEngine;

public class ReTryButton : ButtonBase
{
    [SerializeField] private RectTransform failPanel;

    protected override void ButtonClicked()
    {
        ActionManager.OnTryAgainButtonPressed?.Invoke(failPanel);
        ActionManager.ReloadGame?.Invoke();
    }
}