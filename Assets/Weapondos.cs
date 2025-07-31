using UnityEngine;

public class Weapondos : MonoBehaviour
{
    [Header("Disparo")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float bulletForce = 20f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip disparoClip;

    private bool estaDisparando = false;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            estaDisparando = true;
            Fire(); // llama al método de disparo
        }

        if (Input.GetMouseButtonUp(0))
        {
            estaDisparando = false;

            if (audioSource.isPlaying)
            {
                audioSource.Stop();
                audioSource.loop = false;
            }
        }
    }

    public void Fire()
    {
        // Crear la bala
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        rb.AddForce(firePoint.forward * bulletForce, ForceMode.Impulse);

        // Reproducir sonido en loop si no está ya sonando
        if (audioSource != null && disparoClip != null && !audioSource.isPlaying && estaDisparando)
        {
            audioSource.clip = disparoClip;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
}
