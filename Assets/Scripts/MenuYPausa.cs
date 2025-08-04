using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuYPausa : MonoBehaviour
{
    private bool juegoPausado = false;

    void Update()
    {
        // Ir al menú con M
        if (Input.GetKeyDown(KeyCode.M))
        {
            Time.timeScale = 1f; // Asegurarse que no quede pausado
            Cursor.lockState = CursorLockMode.None; // Liberar cursor
            Cursor.visible = true; // Mostrar cursor
            SceneManager.LoadScene("Menu");
        }

        // Pausar / Reanudar con P
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (juegoPausado)
                ReanudarJuego();
            else
                PausarJuego();
        }
    }

    void PausarJuego()
    {
        Time.timeScale = 0f; // Congelar el tiempo
        juegoPausado = true;
        Cursor.lockState = CursorLockMode.None; // Liberar cursor
        Cursor.visible = true; // Mostrar cursor
    }

    void ReanudarJuego()
    {
        Time.timeScale = 1f; // Reanudar el tiempo
        juegoPausado = false;
        Cursor.lockState = CursorLockMode.Locked; // Bloquear cursor
        Cursor.visible = false; // Ocultar cursor
    }
}
