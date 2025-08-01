using UnityEngine;

public class GroundEnemy : MonoBehaviour
{
    public Transform player;
    public float speed = 3f;
    public float rotationSpeed = 5f;
    public float distanciaAlejarse = 4f;
    public float tiempoEntreAtaques = 1.5f;

    private Animator animator;
    private bool hasComeOut = false;
    private bool yaTelepatia = false;
    private float tiempoUltimoAtaque;
    private bool alejandose = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null) return;

        VidaJugador vidaJugador = player.GetComponent<VidaJugador>();
        if (vidaJugador == null) return;

        // Si el jugador tiene 20 o menos de vida y aún no se hizo la animación
        if (vidaJugador.VidaActual <= 20 && !yaTelepatia)
        {
            yaTelepatia = true;
            alejandose = true;
            animator.Play("Demon|Run1");
            return;
        }

        // Si debe alejarse
        if (alejandose)
        {
            Vector3 direccionAlejarse = (transform.position - player.position).normalized;
            transform.position += direccionAlejarse * speed * Time.deltaTime;

            float distanciaActual = Vector3.Distance(transform.position, player.position);
            if (distanciaActual >= distanciaAlejarse)
            {
                alejandose = false;
                animator.Play("Demon|Telepathic");
                Debug.Log("▶️ Animación TELEPATHIC ejecutada");

                ElevarJugador();

                Invoke(nameof(MatarJugador), 4f);
            }
            return;
        }

        // Comportamiento normal de persecución
        Vector3 direccion = player.position - transform.position;
        direccion.y = 0f;
        float distancia = direccion.magnitude;

        if (!hasComeOut)
        {
            animator.Play("Demon|Come-out2");
            hasComeOut = true;
            return;
        }

        if (distancia > 1.5f)
        {
            Vector3 direccionMov = direccion.normalized;
            transform.position += direccionMov * speed * Time.deltaTime;
            animator.Play("Demon|Run1");
        }

        if (direccion != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void ElevarJugador()
    {
        if (player != null)
        {
            Rigidbody rb = player.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.useGravity = false;
                rb.linearVelocity = Vector3.up * 3f;
            }
        }
    }

    void MatarJugador()
    {
        if (player != null)
        {
            VidaJugador vidaJugador = player.GetComponent<VidaJugador>();
            if (vidaJugador != null)
            {
                vidaJugador.RecibirDaño(vidaJugador.VidaActual);
                Debug.Log("💀 Jugador eliminado por TELEPATÍA");
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.transform == player)
        {
            VidaJugador vidaJugador = player.GetComponent<VidaJugador>();
            if (vidaJugador != null && vidaJugador.VidaActual > 20 && !yaTelepatia)
            {
                if (Time.time >= tiempoUltimoAtaque + tiempoEntreAtaques)
                {
                    animator.Play("Demon|Punch1");
                    vidaJugador.RecibirDaño(20);
                    tiempoUltimoAtaque = Time.time;
                }
            }
        }
    }

    public void RecibirDaño(int cantidad)
    {
        animator.Play("Demon|Get-damage");
    }
}
