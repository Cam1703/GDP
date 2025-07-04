using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _movespeed = 3f;
    [SerializeField] private float _dashSpeed = 15f;
    [SerializeField] private float _dashDuration = 0.5f;
    [SerializeField] private float _dashCooldown = 1f;
    [SerializeField] AudioClip _dashSound;
    [SerializeField] AudioSource _audioSource;

    private Vector2 _movement;
    private bool _isDashing = false;
    private float _dashTimeLeft;
    private float _lastDashTime = -Mathf.Infinity;

    private const string _horizontal = "Horizontal";
    private const string _vertical = "Vertical";

    private Rigidbody2D _rb;
    private Animator animator;
    private SpriteRenderer _spriteRenderer;
    private BoxCollider2D _collider;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _collider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        _movement.Set(InputManager.movement.x, InputManager.movement.y);

        // Dash input
        if (InputManager.dash && !_isDashing && Time.time >= _lastDashTime + _dashCooldown && _movement != Vector2.zero)
        {
            Debug.Log("Dash triggered");
            _isDashing = true;
            _dashTimeLeft = _dashDuration;
            _lastDashTime = Time.time;

            _audioSource.PlayOneShot(_dashSound); //Reproducir sonido al inicio del dash
        }

        if (_isDashing)
        {
            _rb.linearVelocity = _movement.normalized * _dashSpeed;
            _dashTimeLeft -= Time.deltaTime;
            _collider.enabled = false; // Desactivar colisión durante el dash
            CodedDashAnimation();
            if (_dashTimeLeft <= 0f)
            {
                _isDashing = false;
                _collider.enabled = true; // Reactivar colisión al iniciar el dash
            }
        }
        else
        {
            _rb.linearVelocity = _movement * _movespeed;
        }

        animator.SetFloat(_horizontal, _movement.x);
        animator.SetFloat(_vertical, _movement.y);
    }

    private void CodedDashAnimation()
    {
        StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine()
    {
        while (_isDashing)
        {
            _spriteRenderer.color = new Color(1f, 1f, 1f, Mathf.PingPong(Time.time * 10f, 1f)); // Cambia la opacidad del sprite
            yield return null;
        }
        _spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // Restaura la opacidad al final
        yield break;
    }
}
