using UnityEngine;
using UnityEngine.SceneManagement;

public class GuardarUltimaEscena : MonoBehaviour
{
    void Start()
    {
        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
    }
}
