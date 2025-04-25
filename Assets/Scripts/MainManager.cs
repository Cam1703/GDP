using UnityEngine;

public class MainManager : MonoBehaviour
{
    
    public static MainManager instance;

    public static GameManager gameManager;
    public static MenuManager menuManager;

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
        Debug.Log(gameManager);
        //_uiManager = GetComponent<UIManager>();
        menuManager = GetComponent<MenuManager>();
        //_boardManager = GetComponent<BoardManager>();
        //_soundManager = GetComponent<SoundManager>();
    }
}
