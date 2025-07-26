using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public float mouseSensitivity = 2f;

    float currentYRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        currentYRotation += mouseX;

        transform.rotation = Quaternion.Euler(0f, currentYRotation, 0f);
    }
}
