using TMPro;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [SerializeField] GameObject player; // Reference to the player GameObject
    [SerializeField] float speed = 1f; // Speed of the enemy
    [SerializeField] int damage = 0; // Speed of the enemy

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player"); 
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (player == null) { return; }
        Vector2 direction = (player.transform.position - transform.position).normalized; // Calculate direction to the player

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime); // Move towards the player

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("Collision detected with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Player"))
        {
            // Trigger game over or player damage

            PlayerHealthManager playerHealth = collision.gameObject.GetComponent<PlayerHealthManager>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage); // Call the TakeDamage method on the player
            }
            else
            {
                Debug.LogError("PlayerHealthManager component not found on player object.");
            }
                Destroy(gameObject);
        }
    }


}
