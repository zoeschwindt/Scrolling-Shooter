using UnityEngine;
using UnityEngine.UI; 

public class EnemyHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;
    public GameObject prefabAlMorir;
    public Image healthFillImage; 

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        float fillAmount = currentHealth / maxHealth;
        healthFillImage.fillAmount = fillAmount;
    }

    void Die()
    {
        if (prefabAlMorir != null)
        {
            Instantiate(prefabAlMorir, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}
