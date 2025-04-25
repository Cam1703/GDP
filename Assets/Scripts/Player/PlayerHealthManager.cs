using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    private int health = 3;

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            // trigger fin de juego
        }

        //UpdateHealth()
        Debug.Log("Player took damage: " + damage);
    }

    public void Heal(int amount)
    {
        health += amount;
        if (health > 3) // Assuming max health is 3
        {
            health = 3;
        }

        //UpdateHealth()
    }


}
