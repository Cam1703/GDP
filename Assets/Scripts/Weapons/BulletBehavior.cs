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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((_whatDestroysBullet.value & (1 << collision.gameObject.layer)) > 0)
        {
            //Damage enemy

            //Destroy bullet
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
