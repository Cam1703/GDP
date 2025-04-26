using UnityEngine;

public class MainManager : MonoBehaviour
{
    
    public static MainManager instance;

    public static GameManager gameManager;
    public static MenuManager menuManager;
    public static PauseManager pauseManager;
    public static UIManager uiManager;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }

        DontDestroyOnLoad(this);

        gameManager = GetComponent<GameManager>();
        uiManager = GetComponent<UIManager>();
        menuManager = GetComponent<MenuManager>();
        pauseManager = GetComponent<PauseManager>();
        //_boardManager = GetComponent<BoardManager>();
        //_soundManager = GetComponent<SoundManager>();
    }
}
