using UnityEngine;
using TMPro;
using System.Collections;
using System;

public class TypewriterText : MonoBehaviour
{
    [System.Serializable]
    public class TextItem
    {
        public TextMeshProUGUI textMesh;
        [TextArea] public string fullText;
    }

    public TextItem[] texts;
    public float delay = 0.05f;
    public AudioSource typewriterAudio;

    public Action OnFinished;

    private Coroutine blinkCoroutine;

    public void StartTypewriter()
    {
        StopAllCoroutines();
        StartCoroutine(ShowAllTexts());
    }

    IEnumerator ShowAllTexts()
    {
        foreach (var item in texts)
        {
            item.textMesh.gameObject.SetActive(true);
            item.textMesh.text = "";

            // Iniciar parpadeo mientras se escribe
            blinkCoroutine = StartCoroutine(BlinkText(item.textMesh));

            if (typewriterAudio != null && typewriterAudio.clip != null)
            {
                typewriterAudio.loop = true;
                typewriterAudio.Play();
            }

            foreach (char c in item.fullText)
            {
                item.textMesh.text += c;

                // Audio por letra (opcional: usar PlayOneShot)
                if (typewriterAudio != null && !typewriterAudio.isPlaying)
                    typewriterAudio.Play();

                yield return new WaitForSecondsRealtime(delay);
            }

            if (typewriterAudio != null)
            {
                typewriterAudio.Stop();
                typewriterAudio.loop = false;
            }

            // Detener parpadeo
            if (blinkCoroutine != null)
                StopCoroutine(blinkCoroutine);

            // Asegurar que el texto quede visible al final
            item.textMesh.enabled = true;

            yield return new WaitForSecondsRealtime(1f);
        }

        // Desactivar todos los textos juntos al final
        foreach (var item in texts)
        {
            item.textMesh.gameObject.SetActive(false);
        }

        OnFinished?.Invoke();
    }

    IEnumerator BlinkText(TextMeshProUGUI textMesh)
    {
        while (true)
        {
            textMesh.enabled = false;
            yield return new WaitForSecondsRealtime(0.3f);
            textMesh.enabled = true;
            yield return new WaitForSecondsRealtime(0.3f);
        }
    }
}
