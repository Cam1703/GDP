using UnityEngine;

public class BulletBehavior : MonoBehaviour
{
    [SerializeField] private float _bulletVelocity = 15f;
    [SerializeField] private float _destroyTime = 3f;

    [SerializeField] private LayerMask _whatDestroysBullet;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

        SetDestroyTime();
        SetStraightVelocity();
    }

    private void OnCollisionEnter2D(Collision2D collision) // Corrected signature
    {
       if(collision.collider.tag == "Enemy")
       {
            Destroy(gameObject);
       }
 
    }

    private void SetStraightVelocity()
    {
        _rb.linearVelocity = transform.right * _bulletVelocity; // Corrected property name
    }

    private void SetDestroyTime()
    {
        Destroy(gameObject, _destroyTime);
    }
}
