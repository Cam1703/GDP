using UnityEngine;

public class ReadInput : MonoBehaviour
{

    public void ReadName(string name)
    {
        GameManager.instance.playerName = name;
        MenuManager.instance.Begin();
    }
}
