using UnityEngine;

public class Bullett4 : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 5f);
    }

    void OnCollisionEnter(Collision other)
    {
        Enemigo4 enemigo = other.collider.GetComponent<Enemigo4>();
        if (enemigo != null)
        {
            enemigo.RecibirDaño(15); // Cambia si querés otro valor
        }

        Destroy(gameObject);
    }
}
