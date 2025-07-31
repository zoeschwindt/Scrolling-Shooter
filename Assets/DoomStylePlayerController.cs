using UnityEngine;

public class DoomStylePlayerController : MonoBehaviour
{
    public float mouseSensitivity = 2f;
    public bool puedeMover = true;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (!puedeMover || !CameraControlManager.Instance.puedeRotar) return;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);
    }
}
