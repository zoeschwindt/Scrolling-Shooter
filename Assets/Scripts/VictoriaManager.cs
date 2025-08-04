using UnityEngine;

public class VictoriaManager : MonoBehaviour
{
    public int enemigosTotales = 3;
    private int enemigosEliminados = 0;

    public GameObject portalVictoria; // Asignar en Inspector

    void Start()
    {
        // Ocultar el portal y el cursor al iniciar
        if (portalVictoria != null)
            portalVictoria.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void EnemigoEliminado()
    {
        enemigosEliminados++;

        if (enemigosEliminados >= enemigosTotales && portalVictoria != null)
        {
            portalVictoria.SetActive(true);

            // Mostrar el cursor al activar el portal
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }
}
