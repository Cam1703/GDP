using UnityEngine;
using System.Collections;

public class HealerBehaviour : MonoBehaviour
{
    [SerializeField] private int healAmount = 1;
    [SerializeField] private int timeToDestroy = 5; // Time in seconds before the healer is destroyed
    private GameObject player;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

        // Start the destroy coroutine
        StartCoroutine(DestroyAfterTime());
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

    //Destroy Coroutine
    private IEnumerator DestroyAfterTime()
    {
        yield return new WaitForSeconds(timeToDestroy);
        Destroy(gameObject);
    }
}
