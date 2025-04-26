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

    public void ReadName(string name)
    {
        instance.playerName = name;
        MainManager.menuManager.Begin();
    }

}
