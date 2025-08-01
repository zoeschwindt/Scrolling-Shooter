using UnityEngine;
using System.Collections;

public class IntroScreenManager : MonoBehaviour
{
    public CanvasGroup introCanvas;
    public float fadeDuration = 1f;
    public TypewriterText typewriter;
    public AudioSource gameplayMusic;

    private bool textFinished = false;

    // Por defecto la intro se muestra, pero puede cambiar otro script (DeathMenu, etc.)
    public static bool showIntro = true;

    void Start()
    {
        if (showIntro)
        {
            // Mostrar la intro
            introCanvas.alpha = 1f;
            introCanvas.blocksRaycasts = true;
            introCanvas.interactable = true;

            typewriter.OnFinished += () => textFinished = true;
            StartCoroutine(ShowIntro());
        }
        else
        {
            // Saltar la intro y empezar el juego directamente
            introCanvas.alpha = 0f;
            introCanvas.blocksRaycasts = false;
            introCanvas.interactable = false;
            Time.timeScale = 1f;

            if (gameplayMusic != null)
                gameplayMusic.Play();
        }
    }

    IEnumerator ShowIntro()
    {
        // Pausar el juego mientras aparece la intro
        Time.timeScale = 0f;
        AudioListener.pause = false;

        // Iniciar escritura de texto
        typewriter.StartTypewriter();

        // Esperar hasta que el texto termine
        while (!textFinished)
            yield return null;

        // Hacer fade-out del panel
        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            introCanvas.alpha = Mathf.Lerp(1f, 0f, elapsed / fadeDuration);
            yield return null;
        }

        // Ocultar completamente la intro
        introCanvas.alpha = 0f;
        introCanvas.blocksRaycasts = false;
        introCanvas.interactable = false;

        // Reanudar el juego
        Time.timeScale = 1f;

        // Reproducir música de gameplay
        if (gameplayMusic != null)
            gameplayMusic.Play();
    }
}
