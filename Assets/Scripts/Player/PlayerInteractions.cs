using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
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
    }

    private void PlayerHeals(int healing)
    {
        GameManager.gameManager._playerHealth.HealUnit(healing);
    }
}
