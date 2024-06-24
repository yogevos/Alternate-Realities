using System;
using UnityEngine;
using UnityEngine.UI;

public class TheSentinelOfEquilibrium : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;


    private void Start()
    {
        health = maxHealth;
    }
    public void Hit(float damageAmount)
    {
        health -= damageAmount;
        ClampHealth();

        if (health <= 0)
        {
            OnDeath();
        }
        
    }


    public void Damage(float damageAmount)
    {
        Hit(damageAmount);
    }

    void ClampHealth()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    public void OnDeath()
    {
        
        Destroy(gameObject);
    }
}
