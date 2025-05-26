using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth = 100f;
    private float currentHealth;

    public Image healthBarImage;
    public GameObject panelDerrota;

    [Header("Sonido de daño")]
    public AudioSource audioSource;
    public AudioClip hurtSound;

    [Header("Sonido de Game Over")]
    public AudioSource gameOverAudioSource;
    public AudioClip gameOverClip;

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

        if (hurtSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(hurtSound);
        }

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
        panelDerrota.SetActive(true);
        Time.timeScale = 0f;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

       
        if (gameOverClip != null && gameOverAudioSource != null)
        {
            gameOverAudioSource.PlayOneShot(gameOverClip);
        }
    }
}

