using UnityEngine;

public class HealerBehaviour : MonoBehaviour
{
    [SerializeField] private int healAmount = 1;
    private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Trigger game over or player damage
            PlayerHealthManager playerHealth = collision.gameObject.GetComponent<PlayerHealthManager>();
            if (playerHealth != null)
            {
                playerHealth.Heal(healAmount); // Call the Heal method on the player
            }
            else
            {
                Debug.LogError("PlayerHealthManager component not found on player object.");
            }
            Destroy(gameObject);
        }
    }
}
