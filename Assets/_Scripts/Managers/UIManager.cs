using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private RectTransform successPanel;
    [SerializeField] private RectTransform failPanel;

    private GameManager gameManager;

    private void Awake()
    {
        gameManager = GameManager.instance;
    }

    private void Start()
    {
        ActionManager.OnFail += Fail;
        ActionManager.OnSuccess += Success;

        ActionManager.OnTryAgainButtonPressed += ClosePanel;
        ActionManager.OnPlayAgainButtonPressed += ClosePanel;
    }

    private void Fail()
    {
        OpenPanel(failPanel);
    }

    private void Success()
    {
        OpenPanel(successPanel);
    }

    private void OpenPanel(RectTransform panel)
    {
        panel.gameObject.SetActive(true);
    }

    private void ClosePanel(RectTransform panel)
    {
        panel.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        ActionManager.OnFail -= Fail;
        ActionManager.OnSuccess -= Success;

        ActionManager.OnTryAgainButtonPressed -= ClosePanel;
        ActionManager.OnPlayAgainButtonPressed -= ClosePanel;
    }
}