using UnityEngine;

public class BossHealth : MonoBehaviour
{
    public float maxHealth = 1000f;
    private float currentHealth;

    public GameObject victoryPanel; // Asignalo desde el Inspector

    void Start()
    {
        currentHealth = maxHealth;

        if (victoryPanel != null)
            victoryPanel.SetActive(false);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("¡El Boss ha sido derrotado!");
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true); // Mostrar panel de victoria
        }

        Destroy(gameObject); // Destruye el boss si querés
    }
}
