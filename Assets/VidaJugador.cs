using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class VidaJugador : MonoBehaviour
{
    public int vidaMaxima = 25;
    private int vidaActual;

    public Image barraVida;
    public GameObject panelPerdiste;

    public AudioClip sonidoDaño;
    private AudioSource audioSource;

    private JugadorInmunidad jugadorInmunidad;

    [Header("Efectos especiales")]
    public bool activarHumoEnEsteNivel = false;
    public GameObject humoPrefab;
    public Transform puntoHumo;
    private GameObject humoInstanciado;
    public int umbralHumo = 59;
    public int VidaActual => vidaActual;

    [Header("Sonido humo")]
    public AudioClip sonidoHumo;
    public AudioSource audioSourceHumo;

    void Start()
    {
        vidaActual = vidaMaxima;
        ActualizarBarra();

        if (panelPerdiste != null)
            panelPerdiste.SetActive(false);

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();

        jugadorInmunidad = GetComponent<JugadorInmunidad>();
    }

    public void RecibirDaño(int cantidad)
    {
        if (jugadorInmunidad != null && jugadorInmunidad.esInmune)
        {
            Debug.Log("Daño evitado: jugador inmune");
            return;
        }

        vidaActual -= cantidad;

        if (sonidoDaño != null && audioSource != null)
            audioSource.PlayOneShot(sonidoDaño);

        if (activarHumoEnEsteNivel && vidaActual <= umbralHumo && humoInstanciado == null && humoPrefab != null)
        {
            Vector3 posicion = puntoHumo != null ? puntoHumo.position : transform.position;
            humoInstanciado = Instantiate(humoPrefab, posicion, Quaternion.identity, transform);

            if (sonidoHumo != null && audioSourceHumo != null)
            {
                audioSourceHumo.PlayOneShot(sonidoHumo);
            }
        }

        if (vidaActual <= 0)
        {
            vidaActual = 0;
            StartCoroutine(MorirConRetraso());
        }

        ActualizarBarra();
    }

    public void RecibirVida(int cantidad)
    {
        vidaActual += cantidad;
        if (vidaActual > vidaMaxima)
            vidaActual = vidaMaxima;

        ActualizarBarra();
    }

    void ActualizarBarra()
    {
        if (barraVida != null)
        {
            barraVida.fillAmount = (float)vidaActual / vidaMaxima;
        }
    }

    IEnumerator MorirConRetraso()
    {
        Debug.Log("El jugador ha muerto");

        PlayerPrefs.SetString("LastScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        Animator anim = GetComponent<Animator>();
        if (anim != null)
            anim.SetTrigger("Morir");

        // Espera el tiempo de la animación (ajustá este valor al largo de tu animación)
        yield return new WaitForSeconds(1.5f);

        SceneManager.LoadScene("Muerte");
    }
}
