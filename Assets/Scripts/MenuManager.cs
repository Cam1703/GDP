using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    private GameObject _startMenu;
    private GameObject _game;
    private GameObject _pauseMenu;

    public static MenuManager instance;
    public bool _hasStarted = false;

    private void Awake()
    {
        
        if (instance == null && instance != this)
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (instance._hasStarted)
        {
            if (MainManager.gameManager._playerHealth.Health == 0)
            {
                Finish();
            }
            else if (InputManager.menuOpenInput)
            {
                Debug.Log(111);
               if (!MainManager.pauseManager.isPaused)
                {
                    Pause();
                }
            }
            else if (InputManager.menuCloseInput)
            {
                Unpause();
            }
        }
    }

    public void Begin()
    {
        Debug.Log(2);
        if (MainManager.gameManager.playerName != "")
        {
            MainManager.pauseManager.UnpauseGame();
            instance._hasStarted = true;
            instance._startMenu.SetActive(false);
            instance._game.SetActive(true);

        }
    }

    public void Pause() 
    {
        MainManager.pauseManager.PauseGame();
        instance._pauseMenu.SetActive(true);

    }

    public void Unpause()
    {
        MainManager.pauseManager.UnpauseGame();
        instance._pauseMenu.SetActive(false);
    }

    public void Finish()
    {
        GameManager.finalName = GameManager.instance.playerName;
        GameManager.finalScore = GameManager.instance.playerScore;
        SceneManager.LoadScene(2);
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene(1);
        StartCoroutine(waiter());
    }

    IEnumerator waiter()
    {
        yield return new WaitForSecondsRealtime(0.1f);
        instance._startMenu = GameObject.Find("StartContainer");
        instance._pauseMenu = GameObject.Find("PauseContainer");
        instance._game = GameObject.Find("GameProper");

        instance._startMenu.SetActive(true);
        instance._game.SetActive(false);
        instance._pauseMenu.SetActive(false);
    }
    public void OnMenuButton()
    {
        Destroy(GameObject.Find("Managers"));
        SceneManager.LoadScene(0);
    }

    public void OnQuitButton()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else 
        Application.Quit();
#endif
    }

}
