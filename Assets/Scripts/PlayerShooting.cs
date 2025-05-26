using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab;
    public Transform gunLeft;
    public Transform gunRight;
    public float fireRate = 0.1f; 
    public float bulletSpeed = 50f;
    public AudioSource gunAudio;
    private bool isShooting = false;
    private float shootTimer;

    [System.Obsolete]
    void Update()
    {
        if (isShooting)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0f)
            {
                Shoot();
                shootTimer = fireRate;
            }
        }
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isShooting = true;
            shootTimer = 0f;

            if (gunAudio != null && !gunAudio.isPlaying)
                gunAudio.Play();
        }
        else if (context.canceled)
        {
            isShooting = false;

            if (gunAudio != null && gunAudio.isPlaying)
                gunAudio.Stop();
        }
    }

    [System.Obsolete]
    void Shoot()
    {
        
        FireBulletFrom(gunLeft);
        FireBulletFrom(gunRight);
    }
    [System.Obsolete]
    void FireBulletFrom(Transform gun)
    {
        GameObject bullet = Instantiate(bulletPrefab, gun.position, gun.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = gun.forward * bulletSpeed;
        }
    }
}