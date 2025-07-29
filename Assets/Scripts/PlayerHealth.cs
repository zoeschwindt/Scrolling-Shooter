using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vida")]
    public float maxHealth = 100f;
    private float currentHealth;

    [Header("UI")]
    public Image healthBarImage;
    public GameObject panelDerrota;

    [Header("Sonido de daño")]
    public AudioSource audioSource;
    public AudioClip hurtSound;

    [Header("Sonido de Game Over")]
    public AudioSource gameOverAudioSource;
    public AudioClip gameOverClip;

    [Header("Efectos de Humo por Daño")]
    public GameObject smokeEffect1; // < 50
    public GameObject smokeEffect2; // < 35
    public GameObject smokeEffect3; // opcional

    [Header("Sonidos de advertencia")]
    public AudioSource warningAudioSource;
    public AudioClip lowHealthClip;        // < 50
    public AudioClip criticalHealthClip;   // < 35

    private bool lowHealthPlayed = false;
    private bool criticalHealthPlayed = false;

    void Start()
    {
        currentHealth = maxHealth;
        UpdateHealthBar();
        UpdateDamageEffects();
        panelDerrota.SetActive(false);

        if (smokeEffect1 != null) smokeEffect1.SetActive(false);
        if (smokeEffect2 != null) smokeEffect2.SetActive(false);
        if (smokeEffect3 != null) smokeEffect3.SetActive(false);
    }

    public void TakeDamage(float amount)
    {
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHealthBar();
        UpdateDamageEffects();

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

    void UpdateDamageEffects()
    {
        if (currentHealth <= 35)
        {
            if (smokeEffect1 != null) smokeEffect1.SetActive(true);
            if (smokeEffect2 != null) smokeEffect2.SetActive(true);
            if (smokeEffect3 != null) smokeEffect3.SetActive(true);

            if (!criticalHealthPlayed && criticalHealthClip != null && warningAudioSource != null)
            {
                warningAudioSource.PlayOneShot(criticalHealthClip);
                criticalHealthPlayed = true;
            }
        }
        else if (currentHealth <= 50)
        {
            if (smokeEffect1 != null) smokeEffect1.SetActive(true);
            if (smokeEffect2 != null) smokeEffect2.SetActive(false);
            if (smokeEffect3 != null) smokeEffect3.SetActive(false);

            if (!lowHealthPlayed && lowHealthClip != null && warningAudioSource != null)
            {
                warningAudioSource.PlayOneShot(lowHealthClip);
                lowHealthPlayed = true;
            }
        }
        else
        {
            // Vida mayor a 50: desactiva efectos y resetea flags
            if (smokeEffect1 != null) smokeEffect1.SetActive(false);
            if (smokeEffect2 != null) smokeEffect2.SetActive(false);
            if (smokeEffect3 != null) smokeEffect3.SetActive(false);

            lowHealthPlayed = false;
            criticalHealthPlayed = false;
        }
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
