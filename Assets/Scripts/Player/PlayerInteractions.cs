using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            PlayerTakesDmg(1);
        }
        if (collision.gameObject.CompareTag("Healer"))
        {
            PlayerHeals(1);
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
