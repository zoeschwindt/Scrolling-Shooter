using UnityEngine;
using System.Collections;

public class IntroScreenManager : MonoBehaviour
{
    public CanvasGroup introCanvas;
    public float fadeDuration = 1f;
    public TypewriterText typewriter;
    public AudioSource gameplayMusic;

    private bool textFinished = false;

    
    public static bool showIntro = true;

    void Start()
    {
        if (showIntro)
        {
            introCanvas.alpha = 1f;
            introCanvas.blocksRaycasts = true;
            introCanvas.interactable = true;

            typewriter.OnFinished += () => textFinished = true;
            StartCoroutine(ShowIntro());

           
            showIntro = false;
        }
        else
        {
            
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
        Time.timeScale = 0f;
        AudioListener.pause = false;

        typewriter.StartTypewriter();

        while (!textFinished)
            yield return null;

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

        if (gameplayMusic != null)
            gameplayMusic.Play();
    }
}
