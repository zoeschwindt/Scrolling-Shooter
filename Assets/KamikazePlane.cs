using UnityEngine;

public class KamikazePlane : MonoBehaviour
{
    public Transform player;
    public float speed = 15f;
    public GameObject explosionPrefab;
    public float selfDestructDistance = 1.5f;
    public float damageAmount = 100f;

    public float maxHealth = 40f;
    private float currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, -90, 0);

        if (Vector3.Distance(transform.position, player.position) < selfDestructDistance)
        {
            Explode();
        }
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
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        if (ScoreManager.instance != null)
        {
            ScoreManager.instance.AddPoint();
        }

        Destroy(gameObject);
    }

    void Explode()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damageAmount);
        }

        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damageAmount);
            }

            Explode();
        }
        else if (other.CompareTag("PlayerBullet"))
        {
            TakeDamage(10f);
            Destroy(other.gameObject);
        }
    }
}
