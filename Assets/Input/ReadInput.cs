using UnityEngine;

public class ReadInput : MonoBehaviour
{

    public void ReadName(string name)
    {
        GameManager.gameManager.playerName = name;    }
}
