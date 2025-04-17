using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    [SerializeField] private Text _name;
    [SerializeField] private Text _score;

    public void Update()
    {
        Debug.Log("Player: " + GameManager.gameManager.playerName);

        _name.text = ("Player: " + GameManager.gameManager.playerName);
    }
}
