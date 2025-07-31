using UnityEngine;
using TMPro;
using System.Collections;

public class JugadorInmunidad : MonoBehaviour
{
    public int cantidadInmunidades = 0;
    public bool esInmune = false;
    public float duracionInmunidad = 5f;

    [Header("UI")]
    public TextMeshProUGUI textoCantidadUI;  // el número que queda visible
    public TextMeshProUGUI textoAnimadoUI;   // el +1 o -1 que aparece y desaparece

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip sonidoRecolectar;
    public AudioClip sonidoUsar;

    private Coroutine animacionTextoCoroutine;

    void Start()
    {
        ActualizarTextoCantidad();
    }

    void Update()
    {
        if (cantidadInmunidades > 0 && !esInmune && Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(ActivarInmunidad());
        }
    }

    public void ObtenerInmunidad()
    {
        cantidadInmunidades++;
        MostrarTextoAnimado("+1", Color.yellow);
        ActualizarTextoCantidad();

        if (audioSource != null && sonidoRecolectar != null)
            audioSource.PlayOneShot(sonidoRecolectar);
    }

    IEnumerator ActivarInmunidad()
    {
        esInmune = true;
        cantidadInmunidades--;
        MostrarTextoAnimado("-1", Color.red);
        ActualizarTextoCantidad();

        if (audioSource != null && sonidoUsar != null)
            audioSource.PlayOneShot(sonidoUsar);

        yield return new WaitForSeconds(duracionInmunidad);
        esInmune = false;
    }

    void ActualizarTextoCantidad()
    {
        if (textoCantidadUI != null)
        {
            textoCantidadUI.text = cantidadInmunidades.ToString();
        }
    }

    void MostrarTextoAnimado(string texto, Color color)
    {
        if (textoAnimadoUI != null)
        {
            if (animacionTextoCoroutine != null)
                StopCoroutine(animacionTextoCoroutine);
            animacionTextoCoroutine = StartCoroutine(AnimarTexto(texto, color));
        }
    }

    IEnumerator AnimarTexto(string texto, Color color)
    {
        textoAnimadoUI.text = texto;
        textoAnimadoUI.color = color;
        textoAnimadoUI.alpha = 1f;
        textoAnimadoUI.gameObject.SetActive(true);

        float duracion = 1.5f;
        float tiempo = 0f;
        while (tiempo < duracion)
        {
            float alpha = Mathf.Lerp(1f, 0f, tiempo / duracion);
            textoAnimadoUI.alpha = alpha;
            tiempo += Time.deltaTime;
            yield return null;
        }

        textoAnimadoUI.alpha = 0f;
        textoAnimadoUI.gameObject.SetActive(false);
    }
}
