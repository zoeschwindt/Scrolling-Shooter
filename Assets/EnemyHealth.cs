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
            GameObject objetoMuerte = Instantiate(prefabAlMorir, transform.position, Quaternion.identity);

            // Buscar bloque padre con script MoveBlock
            Transform bloquePadre = transform.parent;

            while (bloquePadre != null && bloquePadre.GetComponent<MoveBlock>() == null)
            {
                bloquePadre = bloquePadre.parent;
            }

            if (bloquePadre != null)
            {
                objetoMuerte.transform.SetParent(bloquePadre);
            }
        }

        // Sumamos punto cuando muere el enemigo
        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.AddPoint();
        }

        Destroy(gameObject);
    }
}
