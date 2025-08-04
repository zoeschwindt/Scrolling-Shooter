using UnityEngine;

public class FallingBallSimple : MonoBehaviour
{
    public float timeToFall = 3f;
    private float timer = 0f;
    private bool counting = false;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            timer += Time.deltaTime;

            if (timer >= timeToFall)
            {
                rb.constraints = RigidbodyConstraints.None; // Se cae
                this.enabled = false; // Desactiva el script
            }
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            timer = 0f; // Se reinicia si el jugador se baja antes
        }
    }
}
