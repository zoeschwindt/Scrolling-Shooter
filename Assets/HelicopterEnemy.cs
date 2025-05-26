using UnityEngine;

public class HelicopterEnemy : MonoBehaviour
{
    public Transform player;
    public float stopDistance = 22f;        // Distancia mínima para detenerse
    public float shootingDistance = 30f;    // Distancia a la que empieza a disparar
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

        if (distance > shootingDistance)
        {
            // Muy lejos, solo acercarse (sin disparar)
            MoveTowardsPlayer();
        }
        else if (distance > stopDistance && distance <= shootingDistance)
        {
            // Entre 22 y 30, se acerca y dispara
            MoveTowardsPlayer();

            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
        else
        {
            // Dentro de 22 metros, se detiene y dispara
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }

        RotateTowardsPlayer();
    }

    void MoveTowardsPlayer()
    {
        Vector3 direction = (player.position - transform.position).normalized;
        Vector3 zigzag = transform.right * Mathf.Sin(Time.time * zigzagFrequency) * zigzagAmplitude;
        Vector3 movement = (direction * speed * Time.deltaTime) + (zigzag * Time.deltaTime);
        transform.position += movement;

        AvoidObstacles();
    }

    void RotateTowardsPlayer()
    {
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
                transform.position += Vector3.up * speed * Time.deltaTime;
                break;
            }
        }
    }
}
