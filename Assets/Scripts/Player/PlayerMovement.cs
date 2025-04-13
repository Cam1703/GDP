using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float _movespeed = 3f;

    private Vector2 _movement;

    private Rigidbody2D _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        _movement.Set(InputManager.Movement.x, InputManager.Movement.y);
        _rb.linearVelocity = _movement * _movespeed;
    }
}
