using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    [Header("Rotación")]
    public Vector3 rotationAxis = Vector3.up; // Eje de rotación
    public float rotationSpeed = 90f;         // Grados por segundo
    public float rotationDuration = 2f;       // Tiempo girando
    public float pauseDuration = 1f;          // Tiempo de pausa entre giros

    private float timer = 0f;
    private bool isRotating = true;
    private bool rotatingForward = true;

    void Update()
    {
        timer += Time.deltaTime;

        if (isRotating)
        {
            float step = rotationSpeed * Time.deltaTime;
            transform.Rotate(rotationAxis * (rotatingForward ? step : -step));

            if (timer >= rotationDuration)
            {
                isRotating = false;
                timer = 0f;
            }
        }
        else
        {
            // Pausa
            if (timer >= pauseDuration)
            {
                rotatingForward = !rotatingForward;
                isRotating = true;
                timer = 0f;
            }
        }
    }
}
