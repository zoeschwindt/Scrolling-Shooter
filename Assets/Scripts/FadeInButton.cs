using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInButton : MonoBehaviour
{
    public Button button; // Tu botón
    public float fadeDuration = 1.5f; // Tiempo en segundos para aparecer

    private Image buttonImage;
    private Text buttonText;

    void Start()
    {
        // Obtener el fondo del botón y el texto
        buttonImage = button.GetComponent<Image>();
        buttonText = button.GetComponentInChildren<Text>();

        // Asegurar que empiecen invisibles
        SetAlpha(buttonImage, 0f);
        SetAlpha(buttonText, 0f);

        // Iniciar el fade
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Clamp01(elapsed / fadeDuration);

            SetAlpha(buttonImage, alpha);
            SetAlpha(buttonText, alpha);

            yield return null;
        }
    }

    void SetAlpha(Graphic graphic, float alpha)
    {
        if (graphic != null)
        {
            Color c = graphic.color;
            c.a = alpha;
            graphic.color = c;
        }
    }
}
