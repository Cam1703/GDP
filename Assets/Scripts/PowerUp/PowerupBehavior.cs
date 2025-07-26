using System.Collections;
using UnityEngine;

public class PowerupBehavior : MonoBehaviour
{
    public PowerupEffect powerupEffect;
    public float duration = 5f; // Duration of the power-up effect in seconds
    public AudioClip pickupSound; // Sound to play when the power-up is picked up
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.clip = pickupSound;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Powerup collided with: " + collision.gameObject.name);
        if (collision.CompareTag("Player"))
        {
            gameObject.GetComponent<SpriteRenderer>().enabled = false; // Hide the powerup sprite
            PlaySound(); // Play the pickup sound
            powerupEffect.ApplyEffect(collision.gameObject);
            StartCoroutine(RemovePowerupAfterDuration(collision.gameObject));
        }
    }

    private IEnumerator RemovePowerupAfterDuration(GameObject player)
    {
        Debug.Log("Powerup effect will be removed after: " + duration + " seconds");
        yield return new WaitForSeconds(duration);
        powerupEffect.RemoveEffect(player);
        Destroy(gameObject);

    }

    private void PlaySound()
    {
        if (audioSource != null && pickupSound != null)
        {
            audioSource.PlayOneShot(pickupSound);
        }
        else
        {
            Debug.LogWarning("AudioSource or pickup sound is not set.");
        }
    }

}
