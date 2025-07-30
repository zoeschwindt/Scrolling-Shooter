using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class VictoryScreenManager : MonoBehaviour
{
    public CanvasGroup victoryCanvas;
    public float fadeDuration = 1f;
    public TypewriterText typewriter;
    public Button continuarButton;

    private bool textFinished = false;

    public void MostrarPantallaVictoria()
    {
        StartCoroutine(ShowVictory());
    }

    IEnumerator ShowVictory()
    {
        Time.timeScale = 0f;
        AudioListener.pause = false;

        victoryCanvas.alpha = 0f;
        victoryCanvas.blocksRaycasts = true;
        victoryCanvas.interactable = true;

        float elapsed = 0f;
        while (elapsed < fadeDuration)
        {
            elapsed += Time.unscaledDeltaTime;
            victoryCanvas.alpha = Mathf.Lerp(0f, 1f, elapsed / fadeDuration);
            yield return null;
        }

        victoryCanvas.alpha = 1f;

        continuarButton.gameObject.SetActive(false);
        typewriter.OnFinished = () => textFinished = true;
        typewriter.StartTypewriter();

        while (!textFinished)
            yield return null;

        continuarButton.gameObject.SetActive(true);
        continuarButton.onClick.RemoveAllListeners();
        continuarButton.onClick.AddListener(CargarNivel2);
    }

    public void CargarNivel2()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Nivel2");
    }
}
