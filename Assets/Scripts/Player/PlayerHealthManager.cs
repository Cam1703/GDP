using UnityEngine;
using System.Collections;

public class PlayerHealthManager : MonoBehaviour
{
    public static PlayerHealthManager instance;

    private int maxHealth = GameManager.maxInitialHealth;
    public int health;

    [Header("Audio")]
    public AudioSource _damageSound;
    public AudioClip[] _damageClips;
    [SerializeField] private AudioClip[] healClips; // Sonidos de curación

    [Header("Invulnerability")]
    [SerializeField] private float invulnerabilityTime = 0.85f;
    [SerializeField] private float blinkInterval = 0.1f;
    private bool isInvulnerable = false;

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    [Header("Camera Shake")]
    [SerializeField] private bool enableCameraShake = true;
    [SerializeField] private float shakeDuration = 0.2f;
    [SerializeField] private float shakeMagnitude = 0.15f;
    private Vector3 camOriginalPos;
    private Transform mainCam;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this);
        }
        else
        {
            instance = this;
        }
    }

    private void Start()
    {
        instance.health = maxHealth;

        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;

        if (Camera.main != null)
        {
            mainCam = Camera.main.transform;
            camOriginalPos = mainCam.localPosition;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isInvulnerable) return;

        instance.health -= damage;

        if (_damageClips != null && _damageClips.Length > 0)
        {
            int randomIndex = Random.Range(0, _damageClips.Length);
            _damageSound.PlayOneShot(_damageClips[randomIndex]);
        }

        // Camera shake 🔥
        if (enableCameraShake && mainCam != null)
        {
            StartCoroutine(CameraShake());
        }

        if (instance.health <= 0)
        {
            MainManager.menuManager.Finish();
        }

        MainManager.uiManager.UpdateHealthBar(instance.health);
        Debug.Log("Player took damage: " + damage);

        StartCoroutine(InvulnerabilityFrames());
    }

    public void Heal(int amount)
    {
        if (healClips != null && healClips.Length > 0)
        {
            int randomIndex = Random.Range(0, healClips.Length);
            _damageSound.PlayOneShot(healClips[randomIndex]);
        }

        instance.health += amount;
        if (instance.health > maxHealth)
        {
            instance.health = maxHealth;
        }

        MainManager.uiManager.UpdateHealthBar(instance.health);
        Debug.Log("Player healed " + amount);
    }

    private IEnumerator InvulnerabilityFrames()
    {
        isInvulnerable = true;
        float elapsed = 0f;

        while (elapsed < invulnerabilityTime)
        {
            // Alterna rojo translúcido y color original
            if (spriteRenderer.color == originalColor)
            {
                spriteRenderer.color = new Color(1f, 0f, 0f, 0.5f);
            }
            else
            {
                spriteRenderer.color = originalColor;
            }

            yield return new WaitForSeconds(blinkInterval);
            elapsed += blinkInterval;
        }

        spriteRenderer.color = originalColor;
        isInvulnerable = false;
    }

    private IEnumerator CameraShake()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float x = Random.Range(-1f, 1f) * shakeMagnitude;
            float y = Random.Range(-1f, 1f) * shakeMagnitude;

            mainCam.localPosition = camOriginalPos + new Vector3(x, y, 0);

            elapsed += Time.deltaTime;
            yield return null;
        }

        mainCam.localPosition = camOriginalPos;
    }
}
