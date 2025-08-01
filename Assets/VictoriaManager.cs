using UnityEngine;

public class VictoriaManager : MonoBehaviour
{
    public int enemigosTotales = 3;
    private int enemigosEliminados = 0;

    public GameObject portalVictoria; // Asignar en Inspector

    void Start()
    {
        if (portalVictoria != null)
            portalVictoria.SetActive(false);
    }

    public void EnemigoEliminado()
    {
        enemigosEliminados++;
        if (enemigosEliminados >= enemigosTotales && portalVictoria != null)
        {
            portalVictoria.SetActive(true); // Mostrar el portal
        }
    }
}
