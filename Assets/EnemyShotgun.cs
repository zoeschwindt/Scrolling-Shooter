using UnityEngine;

public class EnemyTurretShotgun : MonoBehaviour
{
    public Transform helicopterTarget;     // El helic�ptero (jugador)
    public float detectionRange = 30f;     // Rango para disparar
    public float fireRate = 1f;            // Disparos por segundo
    public GameObject bulletPrefab;        // Prefab de la bala
    public Transform firePoint;            // El ca��n que gira y dispara

    private float nextFireTime = 0f;

    void Update()
    {
        if (helicopterTarget == null) return;

        float distance = Vector3.Distance(transform.position, helicopterTarget.position);
        if (distance <= detectionRange)
        {
            AimAtTarget();

            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void AimAtTarget()
    {
        Vector3 direction = helicopterTarget.position - firePoint.position;

        if (direction != Vector3.zero)
        {
            // Gira el ca��n para mirar al helic�ptero en 3D
            Quaternion targetRotation = Quaternion.LookRotation(direction.normalized);

            // Rota suavemente hacia el objetivo (puedes cambiar velocidad)
            firePoint.rotation = Quaternion.Slerp(firePoint.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        ShotgunBullet bulletScript = bullet.GetComponent<ShotgunBullet>();
        if (bulletScript != null)
        {
            // Disparar en la direcci�n hacia la que apunta el ca��n
            bulletScript.SetDirection(firePoint.forward);
        }
    }
}