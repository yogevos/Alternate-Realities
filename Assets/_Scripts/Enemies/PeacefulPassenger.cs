using System;
using UnityEngine;

public class PeacefulPassenger : MonoBehaviour, IDamageable
{
    [SerializeField] private float health;
    [SerializeField] private float maxHealth;
    public static Action onPeacefulPassengerDeath;

    private void Start()
    {
        health = maxHealth;
    }


    public void Hit(float damageAmount)
    {
        
        health -= damageAmount;
        if (health <= 0)
        {
            RealityManager.Instance.ChangeReality();
            Destroy(gameObject);
        }
    }
    public void Damage(float damageAmount)
    {
        Hit(damageAmount);
    }

    public void PassengerReachedEnd()
    {
        Destroy(gameObject);
    }
    private void OnDisable()
    {
        PawnManager.Instance.SpawnPassenger();
        // tell RealityManager to swap reality
    }
}