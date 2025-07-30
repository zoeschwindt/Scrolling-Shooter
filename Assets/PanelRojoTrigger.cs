using UnityEngine;

public class PanelRojoTrigger : MonoBehaviour
{
    [Header("Prefabs y Transforms")]
    public GameObject panelVerdePrefab;
    public Transform panelSpawnPoint;

    public GameObject puertaAbiertaPrefab;
    public Transform puertaSpawnPoint;

    public GameObject puertaCerrada;

    [Header("Sonidos")]
    public AudioSource audioPanelRojo;
    public AudioSource audioPuertaAbierta;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && GameManager.instancia.puntosItems == GameManager.instancia.puntosParaGanar)
        {
            // Reproducir sonido del panel rojo
            if (audioPanelRojo != null)
                audioPanelRojo.Play();

            // Resetear ítems
            GameManager.instancia.puntosItems = 0;
            GameManager.instancia.ActualizarTextoPuntosItems();

            // Instanciar panel verde
            if (panelVerdePrefab != null && panelSpawnPoint != null)
                Instantiate(panelVerdePrefab, panelSpawnPoint.position, panelSpawnPoint.rotation);

            // Instanciar puerta abierta
            if (puertaAbiertaPrefab != null && puertaSpawnPoint != null)
            {
                Instantiate(puertaAbiertaPrefab, puertaSpawnPoint.position, puertaSpawnPoint.rotation);

                // Reproducir sonido de puerta abierta
                if (audioPuertaAbierta != null)
                    audioPuertaAbierta.Play();
            }

            // Destruir puerta cerrada
            if (puertaCerrada != null)
                Destroy(puertaCerrada);

            // Destruir este panel rojo
            Destroy(gameObject, 0.1f); // leve delay para que el sonido no se corte
        }
    }
}
