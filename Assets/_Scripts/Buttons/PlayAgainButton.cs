using UnityEngine;

public class PlayAgainButton : ButtonBase
{
    [SerializeField] private RectTransform successPanel;

    protected override void ButtonClicked()
    {
        ActionManager.OnPlayAgainButtonPressed?.Invoke(successPanel);
        ActionManager.ReloadGame?.Invoke();
    }
}