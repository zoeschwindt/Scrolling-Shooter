using UnityEngine;
using System.Collections;

public class MovingPlatform : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 1f;
    public float height = 5f;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip musicaPlataforma;

    private Vector3 startPos;
    private Vector3 endPos;
    private bool jugadorEncima = false;
    private bool bajando = false;
    private bool seDetuvoEnMitad = false;

    void Start()
    {
        startPos = transform.position;
        endPos = startPos - Vector3.up * height;

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (jugadorEncima && !bajando)
        {
            StartCoroutine(BajarPlataforma());
        }
    }

    IEnumerator BajarPlataforma()
    {
        bajando = true;

        if (musicaPlataforma != null && audioSource != null && !audioSource.isPlaying)
        {
            audioSource.clip = musicaPlataforma;
            audioSource.loop = false;
            audioSource.Play();
        }

        Vector3 mitad = Vector3.Lerp(startPos, endPos, 0.5f);
        // Bajamos hasta la mitad
        while (Vector3.Distance(transform.position, mitad) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, mitad, moveSpeed * Time.deltaTime);
            yield return null;
        }

        // Pausa de 5 segundos
        yield return new WaitForSeconds(5f);

        // Seguimos bajando hasta el final
        while (Vector3.Distance(transform.position, endPos) > 0.05f)
        {
            transform.position = Vector3.MoveTowards(transform.position, endPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEncima = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEncima = false;
        }
    }
}
