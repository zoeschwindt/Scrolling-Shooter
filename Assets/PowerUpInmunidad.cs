using UnityEngine;

public class PowerUpInmunidad : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            JugadorInmunidad jugador = other.GetComponent<JugadorInmunidad>();
            if (jugador != null)
            {
                jugador.ObtenerInmunidad();
            }

            

            Destroy(gameObject);
        }
    }
}
