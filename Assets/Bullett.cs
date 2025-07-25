using UnityEngine;

public class Bullett : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter(Collision other)
    {

        VidaEnemigo enemigo = other.collider.GetComponent<VidaEnemigo>();
        if (enemigo != null)
        {
            enemigo.RecibirDaņo(enemigo.daņoRecibidoPorBala);
        }

        Destroy(gameObject);
    }
}
