using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAimAndShoot : MonoBehaviour
{
    [Header("Referencias")]
    [SerializeField] private GameObject _gun;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _bulletSpawnPoint;

    [Header("Fire Settings")]
    [Tooltip("Segundos entre disparos cuando se mantiene pulsado")]
    [SerializeField] private float fireRate = 0.2f;

    [Header("SFX")]
    [SerializeField] private AudioClip _shootSFX;
    [SerializeField] private AudioSource _audioSource;

    private Camera _mainCamera;
    private float _fireCooldown = 0f;

    void Awake()
    {
        _mainCamera = Camera.main;
        if (_mainCamera == null)
            Debug.LogError("[PlayerAimAndShoot] No se encontró Camera.main en la escena.");
    }

    void Update()
    {
        AimGun();
        HandleShooting();
    }

    private void AimGun()
    {
        // Obtener posición del ratón en mundo
        Vector2 mousePos = _mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        // Calcular dirección y rotar la pistola
        Vector2 dir = (mousePos - (Vector2)_gun.transform.position).normalized;
        _gun.transform.right = dir;
    }

    private void HandleShooting()
    {
        // Decrementar el cooldown
        _fireCooldown -= Time.deltaTime;

        // Usamos el flag de “held” en lugar de WasPerformedThisFrame
        if (InputManager.attackHeld)
        {
            if (_fireCooldown <= 0f)
            {
                Shoot();
                _fireCooldown = fireRate;
            }
        }
        else
        {
            _fireCooldown = 0f;
        }
    }


    private void Shoot()
    {
        // Reproducir sonido de disparo
        if (_audioSource != null && _shootSFX != null)
        {
            _audioSource.PlayOneShot(_shootSFX);
        }
        Instantiate(
            _bulletPrefab,
            _bulletSpawnPoint.position,
            _gun.transform.rotation
        );
    }
}
