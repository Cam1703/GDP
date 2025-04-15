using UnityEngine;
using UnityEngine.UI;

public class PlayerInteractions : MonoBehaviour
{

    public Image[] hearts;
    public Sprite filledHeart;
    public Sprite emptyHeart;

    void Start()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < GameManager.gameManager._playerHealth.MaxHealth)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerTakesDmg(1);
            Debug.Log(GameManager.gameManager._playerHealth.Health);   
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            PlayerHeals(1);
            Debug.Log(GameManager.gameManager._playerHealth.Health);
        }
    }

    private void PlayerTakesDmg(int dmg)
    {
        GameManager.gameManager._playerHealth.DmgUnit(dmg);
        for (int i = 0; i < dmg; i++)
        {
            hearts[GameManager.gameManager._playerHealth.Health - i].sprite = emptyHeart;
        }
    }

    private void PlayerHeals(int healing)
    {
        GameManager.gameManager._playerHealth.HealUnit(healing);
        for (int i = healing; i > 0 ; i--)
        {
            hearts[GameManager.gameManager._playerHealth.Health - i].sprite = filledHeart;
        }
    }
}
