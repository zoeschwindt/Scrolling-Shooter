using System.Collections;
using UnityEngine;

public class EnemyNivel3 : MonoBehaviour
{
    public Transform objetivo;
    public float distanciaParaPerseguir = 40f;
    public float distanciaMinimaAlJugador = 2f;
    public float velocidadMovimiento = 5f;
    public float tiempoChequeo = 0.2f;

    [Header("Disparo")]
    public GameObject proyectilPrefab;
    public Transform puntoDisparo;
    public float tiempoEntreDisparos = 1.5f;

    private float tiempoUltimoDisparo;

    void Start()
    {
        StartCoroutine(RevisarDistancia());
    }

    IEnumerator RevisarDistancia()
    {
        while (true)
        {
            if (objetivo != null)
            {
                float distancia = Vector3.Distance(transform.position, objetivo.position);

                if (distancia <= distanciaParaPerseguir && distancia > distanciaMinimaAlJugador)
                {
                    Vector3 direccion = (objetivo.position - transform.position).normalized;
                    direccion.y = 0; // mantener en plano horizontal
                    transform.position += direccion * velocidadMovimiento * Time.deltaTime;

                    if (direccion != Vector3.zero)
                    {
                        Quaternion rotacion = Quaternion.LookRotation(direccion);
                        transform.rotation = Quaternion.Slerp(transform.rotation, rotacion, Time.deltaTime * 10f);
                    }
                }

                if (distancia <= distanciaParaPerseguir && Time.time >= tiempoUltimoDisparo)
                {
                    Disparar();
                    tiempoUltimoDisparo = Time.time + tiempoEntreDisparos;
                }
            }

            yield return new WaitForSeconds(tiempoChequeo);
        }
    }

    void Disparar()
    {
        if (proyectilPrefab != null && puntoDisparo != null && objetivo != null)
        {
            GameObject proyectil = Instantiate(proyectilPrefab, puntoDisparo.position, puntoDisparo.rotation);
            Rigidbody rb = proyectil.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 direccion = (objetivo.position - puntoDisparo.position).normalized;
                direccion.y = 0;
                rb.linearVelocity = direccion * 50f;
            }
        }
    }
}
