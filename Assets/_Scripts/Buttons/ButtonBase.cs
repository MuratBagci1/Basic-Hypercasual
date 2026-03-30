using UnityEngine;
using UnityEngine.UI;

public class ButtonBase : MonoBehaviour
{
    protected Button button;

    public void Awake()
    {
        button = GetComponent<Button>();
    }

    private void Start()
    {
        button.onClick.AddListener(ButtonClicked);
    }

    protected virtual void ButtonClicked() { }
    protected virtual void DisableButton()
    {
        gameObject.SetActive(false);
    }
}