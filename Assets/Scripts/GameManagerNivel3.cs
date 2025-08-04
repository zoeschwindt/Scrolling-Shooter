using UnityEngine;
using TMPro;
using System.Collections;

public class GameManagerNivel3 : MonoBehaviour
{
    public static GameManagerNivel3 instancia;

    [Header("Puntos")]
    public int puntosEnemigos = 0;
    public int puntosItems = 0;

    [Header("UI")]
    public TextMeshProUGUI textoPuntosEnemigos;
    public TextMeshProUGUI textoPuntosItems;

    [Header("Paneles")]
    public GameObject panelRojo;

    private void Awake()
    {
        if (instancia == null) instancia = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        Time.timeScale = 1f;

        if (panelRojo != null) panelRojo.SetActive(false);

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

    void ActualizarTextoPuntosItems()
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
}
