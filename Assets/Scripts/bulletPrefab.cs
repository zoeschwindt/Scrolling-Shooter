using UnityEngine;

public class bulletPrefab : MonoBehaviour
{
    public float lifeTime = 5f;
    public float speed = 20f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Le damos velocidad en la dirección en la que está rotado el prefab
        rb.linearVelocity = transform.forward * speed;

        // Destruir la bala luego de un tiempo
        Destroy(gameObject, lifeTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Destruir la bala al colisionar con algo
        Destroy(gameObject);
    }
}


