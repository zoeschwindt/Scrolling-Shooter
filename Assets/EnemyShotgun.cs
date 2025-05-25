using UnityEngine;

public class EnemyShotgun : MonoBehaviour
{
    public Transform helicopterTarget;     // Referencia al helicóptero (enemigo volador)
    public float detectionRange = 20f;     // Rango para detectar al helicóptero
    public float fireRate = 1f;            // Disparos por segundo
    public GameObject bulletPrefab;        // Prefab de la bala
    public Transform firePoint;            // Punto desde donde se dispara

    private float nextFireTime = 0f;

    void Update()
    {
        if (helicopterTarget == null) return;

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
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(helicopterTarget.position - firePoint.position));

        // Enviamos la dirección al script de la bala
        ShotgunBullet bulletScript = bullet.GetComponent<ShotgunBullet>();
        if (bulletScript != null)
        {
            bulletScript.SetDirection(helicopterTarget.position);
        }
    }
}
