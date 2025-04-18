using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealthBar : MonoBehaviour
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
        for (int i = 0; i < hearts.Length; i++)
        {
            if (i < GameManager.gameManager._playerHealth.Health)
            {
                hearts[i].sprite = filledHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }
        }
    }
}
