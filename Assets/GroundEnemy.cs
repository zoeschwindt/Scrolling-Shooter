using UnityEngine;
using System.Collections;

public class GroundEnemy : MonoBehaviour
{
    [Header("Vida del Enemigo")]
    public int vidaMaxima = 100;
    private int vidaActual;

    [Header("Stats Movimiento / Ataque")]
    public Transform player;
    public float speed = 3f;
    public float rotationSpeed = 5f;
    public float distanciaDeteccion = 6f; // distancia para activar telepatía
    public float distanciaAlejarse = 4f;
    public float tiempoEntreAtaques = 1.5f;

    [Header("Sonido elevación")]
    public AudioClip sonidoElevacion;
    private AudioSource audioSource;

    private Animator animator;
    private bool hasComeOut = false;
    private bool yaTelepatia = false;
    private bool alejandose = false;
    private bool enTelepatia = false;
    private bool atacando = false;
    private float tiempoUltimoAtaque;

    void Start()
    {
        animator = GetComponent<Animator>();
        vidaActual = vidaMaxima;

        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
            audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (player == null) return;
        VidaJugador vidaJugador = player.GetComponent<VidaJugador>();
        if (vidaJugador == null) return;

        if (enTelepatia || atacando) return;

        float distanciaActual = Vector3.Distance(transform.position, player.position);

        // Detectar si puede iniciar telepatía
        if (!yaTelepatia && vidaJugador.VidaActual <= 20 && distanciaActual <= distanciaDeteccion)
        {
            yaTelepatia = true;
            alejandose = true;
            animator.Play("Demon|Run1");
        }

        if (alejandose)
        {
            Vector3 direccionAlejarse = (transform.position - player.position).normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direccionAlejarse), rotationSpeed * Time.deltaTime);
            transform.position += direccionAlejarse * speed * Time.deltaTime;

            if (Vector3.Distance(transform.position, player.position) >= distanciaAlejarse)
            {
                alejandose = false;
                enTelepatia = true;

                animator.Play("Demon|Telepathic");
                ElevarJugador();
            }
            return;
        }

        if (!hasComeOut)
        {
            animator.Play("Demon|Come-out2");
            hasComeOut = true;
            return;
        }

        // Movimiento normal
        if (vidaJugador.VidaActual > 20 && distanciaActual > 1.5f)
        {
            Vector3 direccion = (player.position - transform.position).normalized;
            transform.position += direccion * speed * Time.deltaTime;
            animator.Play("Demon|Run1");

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(player.position - transform.position), rotationSpeed * Time.deltaTime);
        }
    }

    void ElevarJugador()
    {
        if (player != null)
        {
            // Desactivar control del jugador
            MonoBehaviour movimientoJugador = player.GetComponent<PlayerMove>();
            if (movimientoJugador != null)
                movimientoJugador.enabled = false;

            // Sonido
            if (sonidoElevacion != null && audioSource != null)
                audioSource.PlayOneShot(sonidoElevacion);

            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                StartCoroutine(SubirJugadorYEliminar(rb));
            }
        }
    }

    IEnumerator SubirJugadorYEliminar(Rigidbody rb)
    {
        float tiempo = 0f;
        float duracionElevacion = 2f;

        animator.Play("Demon|Telepathic");

        while (tiempo < duracionElevacion)
        {
            // Mirar al jugador siempre
            Vector3 direccion = (player.position - transform.position).normalized;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direccion), rotationSpeed * Time.deltaTime);

            // Elevar
            rb.linearVelocity = Vector3.up * 3f;

            tiempo += Time.deltaTime;
            yield return null;
        }

        // Soltar y dejar caer
        rb.linearVelocity = Vector3.zero;
        rb.useGravity = true;

        // Esperar caída y luego matar
        yield return new WaitForSeconds(0.5f);
        MatarJugador();
    }

    void MatarJugador()
    {
        if (player != null)
        {
            animator.Play("Demon|Telepathic");
            StartCoroutine(AplicarDañoFatalConDelay(1.0f));
        }
    }

    IEnumerator AplicarDañoFatalConDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        VidaJugador vidaJugador = player.GetComponent<VidaJugador>();
        if (vidaJugador != null)
            vidaJugador.RecibirDaño(vidaJugador.VidaActual);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform == player && !yaTelepatia && !atacando)
        {
            VidaJugador vidaJugador = player.GetComponent<VidaJugador>();
            if (vidaJugador != null && vidaJugador.VidaActual > 20)
            {
                if (Time.time >= tiempoUltimoAtaque + tiempoEntreAtaques)
                    StartCoroutine(AnimarAtaque(vidaJugador));
            }
        }
    }

    IEnumerator AnimarAtaque(VidaJugador vidaJugador)
    {
        atacando = true;
        animator.Play("Demon|Punch1");
        yield return new WaitForSeconds(0.5f);
        vidaJugador.RecibirDaño(20);
        yield return new WaitForSeconds(0.5f);
        atacando = false;
        tiempoUltimoAtaque = Time.time;
    }

    public void RecibirDaño(int cantidad)
    {
        vidaActual -= cantidad;
        animator.Play("Demon|Get-damage");

        if (vidaActual <= 0)
            Muerte();
    }

    void Muerte()
    {
        animator.Play("Demon|Death");
        Destroy(gameObject, 2f);
        FindObjectOfType<VictoriaManager>()?.EnemigoEliminado();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Bullet"))
        {
            RecibirDaño(5);
            Destroy(other.gameObject);
        }
    }
}
