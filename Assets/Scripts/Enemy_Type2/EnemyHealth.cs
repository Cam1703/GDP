using UnityEngine;
using UnityEngine.Audio;
using System.Collections;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private int health = 3;
    [SerializeField] public AudioSource _damageSound;

    public void TakeDamage(int damage)
    {
        health -= damage;
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
