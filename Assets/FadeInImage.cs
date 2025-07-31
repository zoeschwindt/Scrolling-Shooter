using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInImage : MonoBehaviour
{
    public Image image;       // Tu imagen "MORISTE"
    public float fadeDuration = 1.5f; // Tiempo en segundos para aparecer

    void Start()
    {
        // Asegurar que la imagen empiece invisible
        Color c = image.color;
        c.a = 0f;
        image.color = c;

        // Iniciar el fade
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float elapsed = 0f;
        Color c = image.color;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsed / fadeDuration); // Aumenta alpha de 0 a 1
            image.color = c;
            yield return null;
        }
    }
}
