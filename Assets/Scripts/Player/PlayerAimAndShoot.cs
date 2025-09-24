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

    [Header ("PowerUps")]
    [SerializeField] private bool _hasTrippleShot = false;

    [Header("SFX")]
    [SerializeField] private AudioClip _shootSFX;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip[] _tripeShotSFXList;

    private Camera _mainCamera;
    private float _fireCooldown = 0f;

    public bool HasTrippleShot { get => _hasTrippleShot; set => _hasTrippleShot = value; }

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
        if (InputManager.attackHeld && !MainManager.pauseManager.isPaused)
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

        Instantiate(_bulletPrefab, _bulletSpawnPoint.position, _gun.transform.rotation);
        if (_hasTrippleShot) TrippleShot();
    }

    private void TrippleShot()
    {
        Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.Euler(0, 0, 15) * _gun.transform.rotation);
        Instantiate(_bulletPrefab, _bulletSpawnPoint.position, Quaternion.Euler(0, 0, -15) * _gun.transform.rotation);
        if (_audioSource != null && _shootSFX != null)
        {
            var randomIndex = Random.Range(0, _tripeShotSFXList.Length);
            _audioSource.PlayOneShot(_tripeShotSFXList[randomIndex]);
        }
    }


}
