using UnityEngine;
using UnityEngine.SceneManagement;

public class PortalVictoriaTrigger : MonoBehaviour
{
    public string nombreEscenaVictoria = "Victoria 1"; // Exacto como en Build Settings

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nombreEscenaVictoria);
        }
    }
}
