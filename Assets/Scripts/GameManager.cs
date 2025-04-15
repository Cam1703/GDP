using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager gameManager {  get; private set; }

    public static int _maxInitialHealth = 3;

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
