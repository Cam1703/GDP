using UnityEngine;

public class PlayerInteractions : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Healer"))
        {
            PlayerHealthManager playerHealth = GetComponent<PlayerHealthManager>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1);
                //Destroy(collision.gameObject);
            }
        }
    }
}
