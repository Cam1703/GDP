using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] public AudioSource _damageSound;
    private EnemyChase _enemyChase;
    public void TakeDamage(int damage)
    {
        health -= damage;
        _enemyChase = TryGetComponent<EnemyChase>(out _enemyChase) ? _enemyChase : null;
        if (_enemyChase != null)
        {
            _enemyChase.Speed -= 1f;
        }

        if (health <= 0)
        {
            AudioSource.PlayClipAtPoint(_damageSound.clip,transform.position);
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyAfterSound()
    {
        // Wait until the sound has finished playing
        while (_damageSound.isPlaying)
        {
            yield return null;
        }

        // Destroy the object
        Destroy(gameObject);
    }
}
