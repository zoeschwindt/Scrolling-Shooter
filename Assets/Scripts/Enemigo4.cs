using System.Collections;
using UnityEngine;

public class Enemigo4 : MonoBehaviour
{
    [Header("Referencia al jugador")]
    public Transform player;

    [Header("Parámetros de movimiento")]
    public float speed = 5f;
    public float rotationSpeed = 5f;
    public float distanceToStop = 1.5f;

    [Header("Vida del enemigo")]
    public int vida = 100;

    private Animator animator;
    private bool hasRoared = false;
    private bool isAttacking = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (player == null || isAttacking) return;

        float distance = Vector3.Distance(transform.position, player.position);

        if (!hasRoared)
        {
            animator.Play("roar");
            hasRoared = true;
            StartCoroutine(WaitToMove());
            return;
        }

        if (distance > distanceToStop)
        {
            // Rotar hacia el jugador
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(-direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);

            // Mover
            transform.position -= transform.forward * speed * Time.deltaTime;

            animator.Play("Idle 1 0");
        }
    }

    IEnumerator WaitToMove()
    {
        yield return new WaitForSeconds(1.5f); // Tiempo de animación "roar"
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.transform == player)
        {
            isAttacking = true;
            animator.Play("attack 2");

            VidaJugador vidaJugador = player.GetComponent<VidaJugador>();
            if (vidaJugador != null)
            {
                vidaJugador.RecibirDaño(100); // Cambiá el valor si querés que haga más daño
            }
        }

        if (collision.gameObject.CompareTag("Bullet"))
        {
            vida -= 15;

            Destroy(collision.gameObject); // Destruye la bala

            if (vida <= 0)
            {
                Destroy(gameObject); // Destruye al enemigo
            }
        }
    }
    public void RecibirDaño(int cantidad)
    {
        vida -= cantidad;

        if (vida <= 0)
        {
            Destroy(gameObject);
        }
    }

}
