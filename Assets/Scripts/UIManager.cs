using System.Linq;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    private GameObject _hearts;
    private Image[] _healthBar;

    public Sprite _filledHeart;
    public Sprite _emptyHeart;

    private TextMeshProUGUI _name;
    private TextMeshProUGUI _score;

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

    public void InitialHealthBar()
    {
        instance._hearts= GameObject.Find("HealthBar");
        instance._healthBar=instance._hearts.GetComponentsInChildren<Image>();

        for (int i = 0; i < instance._healthBar.Length; i++)
        {
            if (i < GameManager.maxInitialHealth)
            {
                instance._healthBar[i].enabled = true;
            }
            else
            {
                instance._healthBar[i].enabled = false;
            }
        }
    }

    public void UpdateHealthBar(int health)
    {
        for (int i = 0; i < instance._healthBar.Length; i++)
        {
            if (i < health)
            {
                instance._healthBar[i].sprite = _filledHeart;
            }
            else
            {
                instance._healthBar[i].sprite = _emptyHeart;
            }
        }
    }

    public void StartNameAndScore()
    {
        _name = GameObject.Find("PlayerName").GetComponent<TextMeshProUGUI>();
        _score = GameObject.Find("PlayerScore").GetComponent<TextMeshProUGUI>();

        _name.text = ("Player: " + MainManager.gameManager.playerName);
        _score.text = ("Score: " + MainManager.gameManager.playerScore.ToString("F3") + " s");
    }

    public void UpdateScore()
    { 
        
    }

}
