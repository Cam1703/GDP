using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Text _name;
    [SerializeField] private Text _score;

    public void Update()
    {
        _name.text = ("Player: " + GameManager.instance.playerName);
        if (!PauseManager.instance.isPaused)
        {
            GameManager.instance.playerScore += Time.deltaTime;
        }
        _score.text = ("Score: " + GameManager.instance.playerScore.ToString("F3")) + " s";
    }
}
