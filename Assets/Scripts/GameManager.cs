using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance {  get; private set; }

    [SerializeField] public static int _maxInitialHealth = 3;
    
    public string playerName = "";
    public float playerScore = 0f;

    public static string finalName;
    public static float finalScore;

    public UnitHealth _playerHealth = new UnitHealth(_maxInitialHealth, _maxInitialHealth);

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

    public void ReadName(string name)
    {
        instance.playerName = name;
        MenuManager.instance.Begin();
        Debug.Log(1);
    }

}
