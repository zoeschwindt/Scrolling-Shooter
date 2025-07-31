using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMove : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 5f;
    public float jumpForce = 7f;

    [Header("Referencias")]
    public Transform cameraTransform;
    public Weapondos weapon;

    private Rigidbody rb;
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction lookAction;
    private InputAction jumpAction;
    private InputAction shootAction;

    private Vector3 moveDirection;
    private bool isGrounded;
    private bool inputEnabled = true;

    private Animator animator;

    // NUEVO PARA DISPARO CONTINUO
    private bool isShootingHeld = false;
    private float shootCooldown = 0.2f;
    private float shootTimer = 0f;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        animator = GetComponent<Animator>();

        playerInput = GetComponent<PlayerInput>();
        moveAction = playerInput.actions["Move"];
        lookAction = playerInput.actions["Look"];
        jumpAction = playerInput.actions["Jump"];
        shootAction = playerInput.actions["Shoot"];

        shootAction.performed += ctx => isShootingHeld = true;
        shootAction.canceled += ctx => isShootingHeld = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!inputEnabled) return;

        RotateCamera();
        CheckGrounded();

        if (jumpAction != null && jumpAction.triggered && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            animator.SetTrigger("JumpTrig");
        }

        // Animación de disparo activada solo si se mantiene apretado
        animator.SetBool("isShooting", isShootingHeld);

        // Disparar solo si está presionado y pasaron los segundos del cooldown
        if (isShootingHeld)
        {
            shootTimer -= Time.deltaTime;

            if (shootTimer <= 0f)
            {
                weapon.Fire(); // Dispara el arma
                shootTimer = shootCooldown; // Reinicia el tiempo
            }
        }
    }

    [System.Obsolete]
    private void FixedUpdate()
    {
        if (!inputEnabled) return;

        // Solo moverse si no está disparando
        if (!isShootingHeld)
            MovePlayer();
        else
            StopPlayer();
    }

    [System.Obsolete]
    private void StopPlayer()
    {
        // Frena completamente el movimiento en X y Z (mantiene Y por gravedad o salto)
        Vector3 velocity = rb.velocity;
        velocity.x = 0f;
        velocity.z = 0f;
        rb.velocity = velocity;

        animator.SetBool("isMoving", false);
    }




    [System.Obsolete]
    private void MovePlayer()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();

        // Movimiento relativo a la cámara
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        moveDirection = camForward * input.y + camRight * input.x;

        Vector3 velocity = moveDirection.normalized * moveSpeed;
        velocity.y = rb.velocity.y;
        rb.velocity = velocity;

        bool isMoving = moveDirection.magnitude > 0.1f;
        animator.SetBool("isMoving", isMoving);
    }

    private void RotateCamera()
    {
        Vector2 look = lookAction.ReadValue<Vector2>();
        transform.Rotate(Vector3.up * look.x);
    }

    private void CheckGrounded()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        isGrounded = Physics.Raycast(ray, 1.1f);
    }

    [System.Obsolete]
    public void Die()
    {
        inputEnabled = false;
        rb.velocity = Vector3.zero;
        animator.SetTrigger("Morir");
    }
}
