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

    public AudioSource _startSound;
    public AudioSource _forwardSound;
    public AudioSource _backwardSound;
    public AudioSource _gameLoop;

    private void Awake()
    {

        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Update()
    {
        if (instance._hasStarted)
        {
            if (InputManager.menuOpenInput)
            {
               if (!MainManager.pauseManager.isPaused)
                {
                    Pause();
                }
            }
            else if (InputManager.menuCloseInput)
            {
                Unpause();
            }
            MainManager.gameManager.UpdateTimer();
        }
    }

    public void Begin()
    {
        if (MainManager.gameManager.playerName != "")
        {
            MainManager.pauseManager.UnpauseGame();
            instance._hasStarted = true;
            instance._startMenu.SetActive(false);
            instance._game.SetActive(true);
            instance._startSound.Play();
            instance._gameLoop.PlayDelayed(0);
            
            MainManager.uiManager.InitialHealthBar();
            MainManager.gameManager.StartTimer();

        }
    }

    public void Pause() 
    {
        instance._gameLoop.Pause();
        instance._backwardSound.Play();
        MainManager.pauseManager.PauseGame();
        instance._pauseMenu.SetActive(true);
    }

    public void Unpause()
    {
        instance._forwardSound.Play();
        MainManager.pauseManager.UnpauseGame();
        instance._pauseMenu.SetActive(false);
        instance._gameLoop.PlayDelayed(_forwardSound.clip.length);

    }

    public void Finish()
    {
        GameManager.finalName = GameManager.instance.playerName;
        GameManager.finalScore = GameManager.instance.playerScore;
        SceneManager.LoadScene(2);
    }

    public void OnPlayButton()
    {
        instance._forwardSound.Play();
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
        instance._gameLoop.Stop();
        instance._backwardSound.Play();
        Destroy(GameObject.Find("Managers"));
        SceneManager.LoadScene(0);
    }

    IEnumerator backToMenu()
    {
        yield return new WaitForSecondsRealtime(_forwardSound.clip.length);
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
