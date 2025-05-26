using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    public float currentHealth;

    public GameObject victoryPanel;  // Asigná este panel desde el inspector

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true); // Mostrar panel de victoria
        }

        Destroy(gameObject); // Si querés que desaparezca el boss al morir
    }
}
