using UnityEngine;

public class KamikazePlane : MonoBehaviour
{
    public Transform player;
    public float speed = 15f;

    public GameObject explosionPrefab; // opcional, para una explosión
    public float selfDestructDistance = 1.5f;

    void Update()
    {
        if (player == null) return;

        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * speed * Time.deltaTime;

        // Corregir rotación porque el modelo apunta a la derecha (X+)
        transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(0, -90, 0);

        if (Vector3.Distance(transform.position, player.position) < selfDestructDistance)
        {
            Explode();
        }
    }

    void Explode()
    {
        if (explosionPrefab != null)
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }

        // Si querés hacerle daño al jugador, podés agregarlo acá
        // player.GetComponent<PlayerHealth>().TakeDamage(damageAmount);

        Destroy(gameObject);
    }
}
