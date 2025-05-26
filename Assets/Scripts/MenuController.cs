using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
 
    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
      
       
        SceneManager.LoadScene(0);
    }

    
    public void RestartLevel()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    
    public void QuitGame()
    {
        Application.Quit();
    }
}
