using UnityEngine;

public class BalaEnemigo : MonoBehaviour
{
    public int da�oAlJugador = 10;

    void Start()
    {
        Destroy(gameObject, 10f);
    }

    void OnCollisionEnter(Collision other)
    {
        VidaJugador jugador = other.collider.GetComponent<VidaJugador>();
        if (jugador != null)
        {
            jugador.RecibirDa�o(da�oAlJugador);
        }

        Destroy(gameObject);
    }
}
