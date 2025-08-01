using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathMenu : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    public void Retry()
    {
        IntroScreenManager.showIntro = false;
        string lastScene = PlayerPrefs.GetString("LastScene", SceneManager.GetActiveScene().name);
        SceneManager.LoadScene(lastScene);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Juego cerrado");
    }
}
