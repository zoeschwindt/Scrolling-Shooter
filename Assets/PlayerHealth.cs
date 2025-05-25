using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public Image healthBarImage;
    public GameObject panelDerrota;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        panelDerrota.SetActive(false);
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
        if (healthBarImage != null)
            healthBarImage.fillAmount = currentHealth / maxHealth;
    }

    void Die()
    {
        Time.timeScale = 0f; // Pausa el juego
        panelDerrota.SetActive(true);
    }

    // Botón opcional para reiniciar
    public void Reintentar()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
