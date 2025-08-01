using UnityEngine;
using UnityEngine.UI;

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
    public Transform puntoHumo; // lugar donde aparece el humo
    private GameObject humoInstanciado;
    public int umbralHumo = 59;
    public int VidaActual => vidaActual;

    [Header("Sonido humo")]
    public AudioClip sonidoHumo;
    public AudioSource audioSourceHumo; // Público, asignar solo este en inspector

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
            Morir();
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

    void Morir()
    {
        Debug.Log("El jugador ha muerto");

        Animator anim = GetComponent<Animator>();
        if (anim != null)
            anim.SetTrigger("Morir");

        if (panelPerdiste != null)
            panelPerdiste.SetActive(true);

        Time.timeScale = 0f;
    }
}
