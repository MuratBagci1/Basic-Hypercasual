using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ActionManager.ReloadGame += ReLoadGame;
    }

    public void ReLoadGame()
    {
        SceneManager.LoadScene(0);
    }

    private void OnDestroy()
    {
        ActionManager.ReloadGame -= ReLoadGame;
    }
}