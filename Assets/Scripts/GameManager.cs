using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance {  get; private set; }

    [SerializeField] public static int maxInitialHealth = 4;
    
    public string playerName = "";
    public float playerScore = 0f;

    public static string finalName;
    public static float finalScore;

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
    }

    public void StartTimer()
    {
        instance.playerScore = 0f;
        MainManager.uiManager.StartNameAndScore();
    }

    public void UpdateTimer()
    {
        if (!MainManager.pauseManager.isPaused)
        {
            instance.playerScore += Time.deltaTime;
            // Actualiza el marcador cada 0.2 segundos usando un acumulador
            if (!Mathf.Approximately(instance.playerScore, 0f) && Mathf.FloorToInt(instance.playerScore * 5) != Mathf.FloorToInt((instance.playerScore - Time.deltaTime) * 5))
            {
                MainManager.uiManager.UpdateScore();
            }
        }
    }

    public void UpdateScore(float points)
    {
        if (!MainManager.pauseManager.isPaused)
        {
            instance.playerScore += points;
            MainManager.uiManager.UpdateScore();
        }
    }

}
