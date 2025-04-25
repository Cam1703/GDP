using System;
using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float _bulletVelocity = 15f;

    private Rigidbody2D _rb;

    private Vector2 _screenBounds;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        SetStraightVelocity();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        bool isPlayer = collision.gameObject.CompareTag("Player");
        if (!isPlayer)
        {
            Destroy(gameObject);
            if (collision.gameObject.CompareTag("Enemy"))
            {
                EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();
                if (enemyHealth != null)
                {
                    enemyHealth.TakeDamage(1);
                }
            }
        }
    }

    private void SetStraightVelocity()
    {
        _rb.linearVelocity = transform.right * _bulletVelocity; 
    }

    private void Update()
    {
        // destroy the bullet outside the screen
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        if (transform.position.x < _screenBounds.x * -1 || transform.position.x > _screenBounds.x ||
            transform.position.y < _screenBounds.y * -1 || transform.position.y > _screenBounds.y)
        {
            Destroy(gameObject);
        }
    }


}
