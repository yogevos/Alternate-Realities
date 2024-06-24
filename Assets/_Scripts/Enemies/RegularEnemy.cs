using UnityEngine;
using UnityEngine.UI;

public class RegularEnemy : MonoBehaviour, IDamageable
{
    public float health;
    [SerializeField] private float maxHealth;
    [SerializeField] private Slider slider;
    private bool belowHalfHealth = false;


    private void Awake()
    {
        RealityManager.onChangeReality += UpdateHealth;
    }
    private void OnDestroy()
    {
        RealityManager.onChangeReality -= UpdateHealth;
    }
    private void Start()
    {
        GameManager.Instance.player2Alive = true;
        health = maxHealth;
    }

    public void Hit(float damageAmount)
    {
        if (!RealityManager.Instance.reality)
        {
            health -= damageAmount;
            ClampHealth();
        }
        else
        {
            health += damageAmount;
            ClampHealth();
        }

        if (health <= 0)
        {
            if (health <= 0)
            {
                GameManager.Instance.player2Alive = false;
            }
            Destroy(gameObject);
        }
        else if (health <= 50 && health > 0 && !belowHalfHealth)
        {
            GameManager.Instance.UnderHalfHealth();
            belowHalfHealth = true;
        }
    }
    public void Damage(float damageAmount)
    {
        Hit(damageAmount);
    }
    void ClampHealth()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        slider.value = health / maxHealth;
    }

    public void UpdateHealth()
    {
        slider.value = health / maxHealth;
    }
}