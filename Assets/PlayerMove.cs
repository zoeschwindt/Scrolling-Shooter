using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;

    [Header("Cámara")]
    public Transform cameraTransform;
    public float mouseSensitivity = 2f;
    public float verticalClamp = 85f;

    [Header("Salto")]
    public float jumpForce = 7f;
    public Transform groundCheck;
    public float groundCheckRadius = 0.3f;
    public LayerMask groundMask;

    [Header("Arma")]
    public Weapondos weapon; // ← Asigná tu objeto con el script del arma

    private Rigidbody rb;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction shootAction;

    private float xRotation = 0f;
    private bool isGrounded;
    internal bool wallrunning;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        playerInput = GetComponent<PlayerInput>();

        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
        jumpAction = playerInput.actions["Jump"];
        shootAction = playerInput.actions["Shoot"]; // ← Acción "Shoot" del Input System

        shootAction.performed += OnShoot;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnDisable()
    {
        if (shootAction != null)
            shootAction.performed -= OnShoot;
    }

    private void OnShoot(InputAction.CallbackContext context)
    {
        if (weapon != null)
            weapon.Fire();
    }

    private void Update()
    {
        RotateCamera();
        CheckGrounded();

        if (jumpAction != null && jumpAction.triggered && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void RotateCamera()
    {
        Vector2 lookInput = lookAction.ReadValue<Vector2>();

        float mouseX = lookInput.x * mouseSensitivity;
        float mouseY = lookInput.y * mouseSensitivity;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -verticalClamp, verticalClamp);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    private void MovePlayer()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();
        Vector3 moveDirection = transform.forward * input.y + transform.right * input.x;

        Vector3 velocity = moveDirection.normalized * moveSpeed;
        velocity.y = rb.linearVelocity.y;

        rb.linearVelocity = velocity;
    }

    private void CheckGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundMask);
    }
}
