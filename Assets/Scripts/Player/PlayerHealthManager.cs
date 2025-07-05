using UnityEngine;

public class PlayerHealthManager : MonoBehaviour
{
    public static PlayerHealthManager instance;

    private int maxHealth = GameManager.maxInitialHealth;
    public int health;
    public AudioSource _damageSound;
    public AudioClip[] _damageClips;

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
    }

    public void TakeDamage(int damage)
    {
        instance.health -= damage;
        if (_damageClips != null && _damageClips.Length > 0)
        {
            int randomIndex = Random.Range(0, _damageClips.Length);
            _damageSound.PlayOneShot(_damageClips[randomIndex]);
        }
        if (instance.health <= 0)
        {
            MainManager.menuManager.Finish();
        }

        MainManager.uiManager.UpdateHealthBar(instance.health);
        Debug.Log("Player took damage: " + damage);
    }

    public void Heal(int amount)
    {
        instance.health += amount;
        if (instance.health > maxHealth)
        {
            instance.health = maxHealth;
        }

        MainManager.uiManager.UpdateHealthBar(instance.health);
        Debug.Log("Player healed " + amount);
    }
}
