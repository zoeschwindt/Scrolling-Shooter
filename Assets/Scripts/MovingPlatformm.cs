using UnityEngine;

public class MovingPlatformm : MonoBehaviour

{
    [Header("Puntos de movimiento")]
    public Transform pointA;
    public Transform pointB;

    [Header("Velocidad")]
    public float speed = 2f;

    private bool playerOnPlatform = false;
    private Transform targetPoint;

    void Start()
    {
        targetPoint = pointB;
    }

    void Update()
    {
        if (playerOnPlatform)
        {
            // Mover plataforma hacia el punto objetivo
            transform.position = Vector3.MoveTowards(transform.position, targetPoint.position, speed * Time.deltaTime);

            // Si llegó al objetivo, cambiar al otro punto
            if (Vector3.Distance(transform.position, targetPoint.position) < 0.1f)
            {
                targetPoint = targetPoint == pointA ? pointB : pointA;
            }
        }
    }

    // Detectar si el jugador entra en la plataforma
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = true;
            other.transform.SetParent(transform); // El jugador se mueve junto con la plataforma
        }
    }

    // Detectar si el jugador sale
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerOnPlatform = false;
            other.transform.SetParent(null); // Deja de seguir la plataforma
        }
    }
}
