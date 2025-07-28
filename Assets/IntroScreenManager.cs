using UnityEngine;
using System.Collections;

public class IntroScreenManager : MonoBehaviour
{
    public CanvasGroup introCanvas;
    public float fadeDuration = 1f;
    public TypewriterText typewriter;

    public AudioSource gameplayMusic; // Música o sonido del juego después del texto

    private bool textFinished = false;

    void Start()
    {
        introCanvas.alpha = 1f;
        introCanvas.blocksRaycasts = true;
        introCanvas.interactable = true;

        typewriter.OnFinished += () => textFinished = true;
        StartCoroutine(ShowIntro());
    }

    IEnumerator ShowIntro()
    {
        Time.timeScale = 0f;
        AudioListener.pause = false;

        typewriter.StartTypewriter();

        while (!textFinished)
            yield return null;

        // Fade out del panel
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            introCanvas.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            yield return null;
        }

        introCanvas.alpha = 0f;
        introCanvas.blocksRaycasts = false;
        introCanvas.interactable = false;

        Time.timeScale = 1f;

        // Activar música de juego
        if (gameplayMusic != null)
        {
            gameplayMusic.Play();
        }
    }
}
