using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager gameManager {  get; private set; }

    [SerializeField] public static int _maxInitialHealth = 3;
    
    public string playerName = "";
    public float playerScore = 0f;

    public static string finalName;
    public static float finalScore;

    public UnitHealth _playerHealth = new UnitHealth(_maxInitialHealth, _maxInitialHealth);

    void Awake()
    {
        if (gameManager != null && gameManager != this)
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
        }
    }

}
