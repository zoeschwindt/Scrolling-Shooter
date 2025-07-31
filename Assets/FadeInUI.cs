using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeInUI : MonoBehaviour
{
    public Image moristeImage;     // Imagen "MORISTE"
    public Button[] buttons;       // Botones que aparecen después
    public float fadeDuration = 1.5f;
    public float delayBetween = 0.3f; // Tiempo entre cada botón

    void Start()
    {
        // Ocultar MORISTE
        SetAlpha(moristeImage, 0f);

        // Ocultar botones (imagen + texto)
        foreach (Button b in buttons)
        {
            SetAlpha(b.image, 0f);
            SetAlpha(b.GetComponentInChildren<Text>(), 0f);
        }

        StartCoroutine(ShowSequence());
    }

    IEnumerator ShowSequence()
    {
        // 1. Aparecer MORISTE
        yield return StartCoroutine(FadeIn(moristeImage, fadeDuration));

        // 2. Aparecer botones uno por uno
        foreach (Button b in buttons)
        {
            yield return new WaitForSeconds(delayBetween);
            StartCoroutine(FadeIn(b.image, fadeDuration * 0.8f));
            StartCoroutine(FadeIn(b.GetComponentInChildren<Text>(), fadeDuration * 0.8f));
        }
    }

    IEnumerator FadeIn(Graphic uiElement, float duration)
    {
        float elapsed = 0f;
        Color c = uiElement.color;
        c.a = 0f;
        uiElement.color = c;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            c.a = Mathf.Clamp01(elapsed / duration);
            uiElement.color = c;
            yield return null;
        }
    }

    void SetAlpha(Graphic uiElement, float alpha)
    {
        Color c = uiElement.color;
        c.a = alpha;
        uiElement.color = c;
    }
}
