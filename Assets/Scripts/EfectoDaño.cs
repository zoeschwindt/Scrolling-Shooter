using UnityEngine;

public class EfectoDaño : MonoBehaviour
{
    public int daño = 100; // cantidad de vida a quitar

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Obtener el script de vida del jugador
            VidaJugador vidaJugador = other.GetComponent<VidaJugador>();

            if (vidaJugador != null)
            {
                vidaJugador.RecibirDaño(daño);
            }
        }
    }
}
