using UnityEngine;

public class BalaEnemiga : MonoBehaviour
{
    public float lifeTime = 5f;
    public float speed = 20f;
    public float damageAmount = 10f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Usamos velocity (no linearVelocity)
        rb.linearVelocity = transform.forward * speed;

        // La bala se destruye después de un tiempo
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider other)
    {
        // Si choca con el jugador
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damageAmount);
            }
        }

        // Destruimos la bala en cualquier caso
        Destroy(gameObject);
    }
}