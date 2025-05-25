using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    public void Jugar()
    {
        SceneManager.LoadScene("Nivel");
    }

    public void Salir()
    {
        Debug.Log("Saliendo...");
        Application.Quit();
    }

    public void VolverJugar()
    {
        SceneManager.LoadScene("Nivel");
    }

    public void VolverMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Credits()
    {
        SceneManager.LoadScene("Credits");
    }

}
