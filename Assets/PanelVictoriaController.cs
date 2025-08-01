using UnityEngine;

public class PanelVictoriaController : MonoBehaviour
{
    public GameObject panelVictoria;

    void Start()
    {
        if (panelVictoria != null)
            panelVictoria.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void MostrarPanel()
    {
        if (panelVictoria != null)
            panelVictoria.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
