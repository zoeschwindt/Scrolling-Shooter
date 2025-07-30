using UnityEngine;

public class VictoryTrigger : MonoBehaviour
{
    public VictoryScreenManager victoryScreenManager;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            victoryScreenManager.MostrarPantallaVictoria();
            Destroy(gameObject); // opcional: evita que se active varias veces
        }
    }
}
