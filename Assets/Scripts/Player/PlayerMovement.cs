using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] private float _movespeed = 3f;

    private Vector2 _movement;

    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";

    private Rigidbody2D _rb;
    private Animator animator;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        _movement.Set(InputManager.movement.x, InputManager.movement.y);
        _rb.linearVelocity = _movement * _movespeed;

        animator.SetFloat(_horizontal, _movement.x);
        animator.SetFloat(_vertical, _movement.y);
    }
}
