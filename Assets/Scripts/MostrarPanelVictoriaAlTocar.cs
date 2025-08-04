using UnityEngine;
using UnityEngine.SceneManagement;

public class MostrarPanelVictoriaAlTocar : MonoBehaviour
{
   
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("Nivel4");  
        }
    }
}
