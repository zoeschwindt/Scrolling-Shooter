using UnityEngine;

public class HelicopterEnemy : MonoBehaviour
{
    public Transform player;

    void Update()
    {
        if (player == null) return;

        // Rotar para mirar al jugador (solo en el plano horizontal)
        Vector3 lookDirection = player.position - transform.position;
        lookDirection.y = 0; // No rotar en el eje Y (altura)

        if (lookDirection != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(lookDirection);
    }
}