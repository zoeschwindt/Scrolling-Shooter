using UnityEngine;
using UnityEngine.InputSystem;

public class Playermovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float tiltAmountZ = 30f;
    public float tiltAmountX = 20f;
    public float tiltSpeed = 5f;

    Vector2 moveInput;

    void Update()
    {

        Vector3 move = new Vector3(moveInput.x, moveInput.y, 0f);
        transform.Translate(move * moveSpeed * Time.deltaTime, Space.World);



        float targetZRotation = -moveInput.x * tiltAmountZ;
        float targetXRotation = -moveInput.y * tiltAmountX;



        Quaternion targetRotation = Quaternion.Euler(targetXRotation, 0f, targetZRotation);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * tiltSpeed);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }
}

