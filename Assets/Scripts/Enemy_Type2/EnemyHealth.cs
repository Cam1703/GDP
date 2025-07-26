using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] public AudioSource _deathSound;
    private EnemyChase _enemyChase;
    private SpriteRenderer _spriteRenderer;

    public void TakeDamage(int damage)
    {
        health -= damage;
        SpriteAnimationOnDamage();
        AudioSource.PlayClipAtPoint(_deathSound.clip, transform.position);

        _enemyChase = TryGetComponent<EnemyChase>(out _enemyChase) ? _enemyChase : null;
        if (_enemyChase != null)
        {
            _enemyChase.Speed -= 1f;
        }

        if (health <= 0)
        {
            GameManager.instance.UpdateScore(10f); // Update score when enemy is destroyed
            AudioSource.PlayClipAtPoint(_deathSound.clip, transform.position);
            Destroy(gameObject);
        }
    }

    private void SpriteAnimationOnDamage()
    {
        // Obtener el SpriteRenderer (puede estar en el objeto o en sus hijos)
        if (_spriteRenderer == null)
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            if (_spriteRenderer == null)
            {
                _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            }
        }

        if (_spriteRenderer != null)
        {
            // Cambiar color a rojo oscuro y restaurar después de un breve tiempo
            _spriteRenderer.color = new Color(1f, 0.5f, 0.5f, 1f);
            StopAllCoroutines(); // Evitar conflictos si recibe daño rápidamente
            StartCoroutine(FlashSprite());
        }
    }

    private IEnumerator FlashSprite()
    {
        yield return new WaitForSeconds(0.1f); // Tiempo del parpadeo
        ResetSpriteColor();
    }

    private void ResetSpriteColor()
    {
        if (_spriteRenderer != null)
        {
            _spriteRenderer.color = Color.white; // Restaura el color original
        }
    }
}
