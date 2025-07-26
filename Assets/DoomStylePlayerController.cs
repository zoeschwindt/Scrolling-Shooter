using UnityEngine;
using UnityEngine.InputSystem;

public class DoomStylePlayerController : MonoBehaviour
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

        // Rota todo el jugador (y por ende también cámara y arma)
        transform.Rotate(Vector3.up * mouseX);
    }
}
