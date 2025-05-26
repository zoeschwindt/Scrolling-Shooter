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

       
        rb.linearVelocity = transform.forward * speed;

        
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter(Collider other)
    {
  
        if (other.CompareTag("Player"))
        {
            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(damageAmount);
            }
        }

        
        Destroy(gameObject);
    }
}