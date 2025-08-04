using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            IntroScreenManager.showIntro = true;

            
            SceneManager.LoadScene("Nivel3");
        }
    }
}
