using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float fallSpeed = 1f;

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Humvee"))
        {
            Destroy(other.gameObject); // Destruye el Humvee
            Destroy(gameObject); // Destruye la bomba también, si querés
        }
        else if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject);        // Solo destruye la bomba
        }

    }
}
