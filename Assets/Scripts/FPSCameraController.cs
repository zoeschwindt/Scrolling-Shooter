using UnityEngine;

public class FPSCameraController : MonoBehaviour
{
    public float mouseSensitivity = 2f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;

        // Girar horizontalmente todo el objeto (jugador, cámara, arma)
        transform.Rotate(Vector3.up * mouseX);
    }
}
