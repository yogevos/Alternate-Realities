using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour, IDamageable
{

    [SerializeField] private float alternateHealth;
    public float mainHealth;
    [SerializeField] private float maxHealth = 200;
    private bool belowHalfHealth = false;

    [SerializeField] private Slider slider;
    //[SerializeField] private Slider alternateSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        RealityManager.onChangeReality += UpdateHealth;
    }
    private void OnDestroy()
    {
        RealityManager.onChangeReality -= UpdateHealth;
    }
    void Start()
    {
        GameManager.Instance.player1Alive = true;
        mainHealth = maxHealth;
        alternateHealth = maxHealth;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            TakeDamage(20);
        }
        if(Input.GetKeyDown(KeyCode.H))
        {
            Heal(10);
        }
    }

    public void TakeDamage(float damage)
    {
        if (!RealityManager.Instance.reality)
        {
            mainHealth -= damage;
            mainHealth = Mathf.Clamp(mainHealth, 0, maxHealth);
            slider.value = mainHealth / maxHealth;

            if (mainHealth <= 0)
            {
                GameManager.Instance.player1Alive = false;
            }
            //alternateHealth += damage;
            //alternateHealth = Mathf.Clamp(alternateHealth, 0, maxHealth);
            //alternateSlider.value = alternateHealth / maxHealth;
        }
        else if (mainHealth <= 50 && mainHealth > 0 && !belowHalfHealth)
        {
            GameManager.Instance.UnderHalfHealth();
            belowHalfHealth = true;
        }
        else
        {
            //mainHealth += damage;
            //mainHealth = Mathf.Clamp(mainHealth, 0, maxHealth);
            //mainSlider.value = mainHealth / maxHealth;

            //alternateHealth -= damage;
            //alternateHealth = Mathf.Clamp(alternateHealth, 0, maxHealth);
            //alternateSlider.value = alternateHealth / maxHealth;
        }

        
    }

    public void Heal(float healAmount)
    {
        if (!RealityManager.Instance.reality)
        {
            mainHealth += healAmount;
            mainHealth = Mathf.Clamp(mainHealth, 0, maxHealth);
            slider.value = mainHealth / maxHealth;

            //alternateHealth -= healAmount;
            //alternateHealth = Mathf.Clamp(alternateHealth, 0, maxHealth);
            //alternateSlider.value = alternateHealth / maxHealth;
        }
        else
        {
            //mainHealth -= healAmount;
            //mainHealth = Mathf.Clamp(mainHealth, 0, maxHealth);
            //mainSlider.value = mainHealth / maxHealth;

            //alternateHealth += healAmount;
            //alternateHealth = Mathf.Clamp(alternateHealth, 0, maxHealth);
            //alternateSlider.value = alternateHealth / maxHealth;
        }
    }
    public void Damage(float damageAmount)
    {
        TakeDamage(damageAmount);
    }

    public void UpdateHealth()
    {
        slider.value = mainHealth / maxHealth;
    }

    public void Buff()
    {

    }

    public enum BuffType
    {

    }

    public enum DebuffType
    {

    }
}
