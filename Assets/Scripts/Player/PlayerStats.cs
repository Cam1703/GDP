using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Text _name;
    [SerializeField] private Text _score;

    public void Update()
    {
        _name.text = ("Player: " + GameManager.gameManager.playerName);
        if (!PauseManager.instance.isPaused)
        {
            GameManager.gameManager.playerScore += Time.deltaTime;
        }
        _score.text = ("Score: " + GameManager.gameManager.playerScore.ToString("F3")) + " s";
    }
}
