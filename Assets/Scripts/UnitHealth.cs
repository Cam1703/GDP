using UnityEngine;

public class UnitHealth
{
    // Fields
    private int _currentHealth;
    private int _currentMaxHealth;

    //Properties  
    public int MaxHealth
    { 
        get 
        { 
            return _currentMaxHealth;
        } 
        
        set 
        {
            _currentMaxHealth = value;
        } 
    }

    public int Health
    {
        get
        {
            return _currentHealth;
        }

        set
        {
            _currentHealth = value;
        }
    }

    // Constructor
    public UnitHealth(int health, int maxHealth)
    {
        _currentHealth = health;
        _currentMaxHealth = maxHealth;
    }

    // Methods
    public void DmgUnit(int dmgAmount)
    {
        if(_currentHealth > 0)
        {
            _currentHealth -= dmgAmount;
        }
    }

    public void HealUnit(int healAmount)
    {
        if (_currentHealth < _currentMaxHealth)
        {
            _currentHealth += healAmount;
        }
        if (_currentHealth > _currentMaxHealth)
        {
            _currentHealth = _currentMaxHealth;
        }
    }
}
