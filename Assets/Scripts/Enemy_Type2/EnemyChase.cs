using TMPro;
using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    [SerializeField] GameObject player; // Reference to the player GameObject
    [SerializeField] float speed = 1f; // Speed of the enemy

    private Vector2 movement;
    private Rigidbody2D rb;

    [SerializeField] private int lives = 2; // Number of lives the enemy has
    private Transform playerTransform;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("PLayerMovement"); 
        }

        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized; // Calculate direction to the player

        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime); // Move towards the player

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Bullet"))
        {
            lives -= 1;

            if (lives <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
