
public class ttpButton : ButtonBase
{
    protected override void ButtonClicked() 
    {
        ActionManager.OnTapToPlayPressed?.Invoke();

        base.DisableButton();
    }    
}