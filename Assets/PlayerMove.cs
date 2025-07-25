using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private Transform cameraTransform;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private Rigidbody rb;

    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions.FindAction("Move");
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MoveRelativeToCamera();
        RotateWithCamera();
    }

    void MoveRelativeToCamera()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 inputDir = new Vector3(input.x, 0f, input.y);

        // Dirección relativa a la cámara
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        Vector3 moveDirection = camForward * inputDir.z + camRight * inputDir.x;

        if (moveDirection.sqrMagnitude > 0.01f)
        {
            rb.MovePosition(rb.position + moveDirection * speed * Time.fixedDeltaTime);
        }
    }

    void RotateWithCamera()
    {
        Vector3 camForward = cameraTransform.forward;
        camForward.y = 0f;

        if (camForward.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(camForward);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.fixedDeltaTime);
        }
    }
}
