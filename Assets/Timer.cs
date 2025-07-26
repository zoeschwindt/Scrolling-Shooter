using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI textoTiempo;
    public GameObject pantallaDerrota;
    public float tiempo = 5 * 60f; // 5 minutos en segundos

    private bool tiempoTerminado = false;

    void Start()
    {
        if (pantallaDerrota != null)
            pantallaDerrota.SetActive(false);
    }

    void Update()
    {
        if (tiempoTerminado) return;

        tiempo -= UnityEngine.Time.deltaTime;

        if (tiempo <= 0f)
        {
            tiempo = 0f;
            tiempoTerminado = true;
            textoTiempo.text = "0:00";
            textoTiempo.color = new Color(textoTiempo.color.r, textoTiempo.color.g, textoTiempo.color.b, 1f);
            if (pantallaDerrota != null)
                pantallaDerrota.SetActive(true);
            return;
        }

        // Calcular minutos y segundos
        int minutos = Mathf.FloorToInt(tiempo / 60f);
        int segundos = Mathf.FloorToInt(tiempo % 60f);
        textoTiempo.text = minutos.ToString("0") + ":" + segundos.ToString("00");

        // Parpadeo cuando queda menos de 1 minuto
        if (tiempo <= 60f)
        {
            float alpha = Mathf.Abs(Mathf.Sin(UnityEngine.Time.time * 5f));
            Color color = textoTiempo.color;
            textoTiempo.color = new Color(color.r, color.g, color.b, alpha);
        }
    }
}
