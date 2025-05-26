using UnityEngine;

public class EnemyShotgun : MonoBehaviour
{
    public float detectionRange = 20f;
    public float fireRate = 1f;
    public GameObject bulletPrefab;
    public Transform firePoint;

    private Transform helicopterTarget;
    private float nextFireTime = 0f;

    void Start()
    {
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            helicopterTarget = player.transform;
        }
        else
        {
            Debug.LogWarning("No se encontró un GameObject con el tag 'Player'.");
        }
    }

    void Update()
    {
       
        if (helicopterTarget == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                helicopterTarget = player.transform;
            }
            return; 
        }

        float distance = Vector3.Distance(transform.position, helicopterTarget.position);
        if (distance <= detectionRange)
        {
            AimAtHelicopter();

            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void AimAtHelicopter()
    {
        Vector3 direction = helicopterTarget.position - transform.position;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(
            bulletPrefab,
            firePoint.position,
            Quaternion.LookRotation(helicopterTarget.position - firePoint.position)
        );

        ShotgunBullet bulletScript = bullet.GetComponent<ShotgunBullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(helicopterTarget.position);
        }
    }
}
