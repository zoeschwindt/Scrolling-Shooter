using UnityEngine;

public class HelicopterEnemy : MonoBehaviour
{
    public Transform player;
    public float stopDistance = 10f;
    public float speed = 5f;

    public GameObject bulletPrefab;
    public Transform[] firePoints;
    public float fireRate = 10f; // 10 balas por segundo

    public float zigzagFrequency = 2f;
    public float zigzagAmplitude = 1f;

    public float obstacleCheckDistance = 5f;

    private float nextFireTime;

    void Start()
    {
        if (player == null)
        {
            GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
            if (playerObject != null)
            {
                player = playerObject.transform;
            }
        }
    }

    void Update()
    {
        if (player == null) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > stopDistance)
        {
            // Dirección base hacia el jugador
            Vector3 direction = (player.position - transform.position).normalized;

            // Zigzag lateral (eje X local del helicóptero)
            Vector3 zigzag = transform.right * Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude;

            // Movimiento combinado
            Vector3 movement = (direction * speed * Time.deltaTime) + (zigzag * Time.deltaTime);

            // Aplicar movimiento y evitar obstáculos
            transform.position += movement;

            AvoidObstacles();
        }
        else
        {
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }

        // Rotar para mirar al jugador (solo en el eje Y)
        Vector3 lookDirection = player.position - transform.position;
        lookDirection.y = 0;
        if (lookDirection != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(lookDirection);
    }

    void Shoot()
    {
        foreach (Transform point in firePoints)
        {
            Instantiate(bulletPrefab, point.position, point.rotation);
        }
    }

    void AvoidObstacles()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, obstacleCheckDistance);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Obstacle") || hit.CompareTag("PlayerBullet"))
            {
                // Elevarse para evitar el obstáculo
                transform.position += Vector3.up * speed * Time.deltaTime;
                break;
            }
        }
    }
}
