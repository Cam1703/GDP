using TMPro;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [SerializeField] GameObject player; // Reference to the player GameObject
    [SerializeField] float speed = 1f; // Speed of the enemy
    [SerializeField] int damage = 1; // Speed of the enemy

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
        Vector2 direction = (player.transform.position - transform.position).normalized; // Calculate direction to the player

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime); // Move towards the player

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            // Trigger game over or player damage
            player.GetComponent<PlayerHealthManager>().TakeDamage(damage); // Assuming the player has a method to take damage
            Destroy(gameObject);
        }
    }


}
