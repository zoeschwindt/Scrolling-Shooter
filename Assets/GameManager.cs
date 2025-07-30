using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager instancia;

    [Header("Puntos")]
    public int puntosEnemigos = 0;
    public int puntosItems = 0;
    public int puntosParaGanar = 5; // Se usa para contar baterías

    [Header("UI")]
    public TextMeshProUGUI textoPuntosEnemigos;
    public TextMeshProUGUI textoPuntosItems;

    [Header("Paneles")]
    public GameObject panelRojo;
    public GameObject panelVictoria; // Asigná en el Inspector el panel de victoria

    private void Awake()
    {
        if (instancia == null) instancia = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        Time.timeScale = 1f;

        if (panelRojo != null) panelRojo.SetActive(false);
        if (panelVictoria != null) panelVictoria.SetActive(false);

        ActualizarTextoPuntosEnemigos();
        ActualizarTextoPuntosItems();
    }

    public void SumarPuntoEnemigo()
    {
        puntosEnemigos++;
        ActualizarTextoPuntosEnemigos();

        if (panelRojo != null)
            StartCoroutine(MostrarPanelRojo());
    }

    public void SumarPuntoItem()
    {
        puntosItems++;
        ActualizarTextoPuntosItems();

        
    }

    void ActualizarTextoPuntosEnemigos()
    {
        if (textoPuntosEnemigos != null)
            textoPuntosEnemigos.text = puntosEnemigos.ToString();
    }

    public void ActualizarTextoPuntosItems()
    {
        if (textoPuntosItems != null)
            textoPuntosItems.text = puntosItems.ToString();
    }

    IEnumerator MostrarPanelRojo()
    {
        panelRojo.SetActive(true);
        yield return new WaitForSeconds(1f);
        panelRojo.SetActive(false);
    }

    void MostrarVictoria()
    {
        if (panelVictoria != null) panelVictoria.SetActive(true);
        Time.timeScale = 0f;
    }
}
