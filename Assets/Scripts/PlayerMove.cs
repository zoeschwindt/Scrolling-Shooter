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
    public Transform groundCheck;
    public LayerMask groundLayer;
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

    // Disparo continuo
    private bool isShootingHeld = false;
    private float shootCooldown = 0.2f;
    private float shootTimer = 0f;

    // WALLRUN
    [Header("Wallrun")]
    public float wallRunForce = 5f;
    public float wallJumpForce = 7f;
    public float wallDetectionDistance = 1f;
    public LayerMask wallLayer;
    private bool isWallRunning = false;
    private bool wallOnRight = false;
    private bool wallOnLeft = false;
    private Vector3 lastWallNormal;

    // WALLCLIMB
    [Header("Wallclimb")]
    public float climbSpeed = 3f;
    public float climbRayLength = 1f;
    private bool isClimbing = false;

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
        CheckForWall();

        if (jumpAction.triggered)
        {
            if (isWallRunning)
                WallJump();
            else if (isGrounded)
                Jump();
            else if (CanClimb())
                isClimbing = true;
        }

        animator.SetBool("isShooting", isShootingHeld);

        if (isShootingHeld)
        {
            shootTimer -= Time.deltaTime;
            if (shootTimer <= 0f)
            {
                weapon.Fire();
                shootTimer = shootCooldown;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!inputEnabled) return;

        if (isClimbing)
        {
            ClimbWall();
            return;
        }

        if (isWallRunning)
        {
            WallRun();
            return;
        }

        if (!isShootingHeld)
            MovePlayer();
        else
            StopPlayer();
    }

    private void StopPlayer()
    {
        Vector3 velocity = rb.linearVelocity;
        velocity.x = 0f;
        velocity.z = 0f;
        rb.linearVelocity = velocity;
        animator.SetBool("isMoving", false);
    }

    private void MovePlayer()
    {
        Vector2 input = moveAction.ReadValue<Vector2>();

        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        moveDirection = camForward * input.y + camRight * input.x;
        Vector3 velocity = moveDirection.normalized * moveSpeed;
        velocity.y = rb.linearVelocity.y;
        rb.linearVelocity = velocity;

        animator.SetBool("isMoving", moveDirection.magnitude > 0.1f);
    }

    private void RotateCamera()
    {
        Vector2 look = lookAction.ReadValue<Vector2>();
        transform.Rotate(Vector3.up * look.x);
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetTrigger("JumpTrig");
    }

    private void CheckGrounded()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, groundLayer);
    }

    private void CheckForWall()
    {
        wallOnRight = Physics.Raycast(transform.position, transform.right, out RaycastHit rightHit, wallDetectionDistance, wallLayer);
        wallOnLeft = Physics.Raycast(transform.position, -transform.right, out RaycastHit leftHit, wallDetectionDistance, wallLayer);

        Vector2 input = moveAction.ReadValue<Vector2>();

        if ((wallOnRight || wallOnLeft) && !isGrounded && !isClimbing && input.y > 0.1f)
        {
            isWallRunning = true;
            rb.useGravity = false;
            lastWallNormal = wallOnRight ? rightHit.normal : leftHit.normal;
        }
        else if (!isClimbing)
        {
            isWallRunning = false;
            rb.useGravity = true;
        }
    }

    private void WallRun()
    {
        Vector3 wallForward = Vector3.Cross(lastWallNormal, Vector3.up);
        if (Vector3.Dot(wallForward, transform.forward) < 0)
            wallForward = -wallForward;

        Vector3 wallRunVelocity = wallForward.normalized * wallRunForce;
        wallRunVelocity.y = 0.5f; // mantener flotando

        rb.linearVelocity = wallRunVelocity;

        animator.SetBool("isMoving", true);
    }

    private void WallJump()
    {
        // Dirección de salto: alejarse de la pared + fuerte impulso hacia arriba
        Vector3 jumpDirection = (lastWallNormal * 1.5f + Vector3.up * 2f).normalized;

        // Cancelar velocidad previa para no arrastrar movimiento
        rb.linearVelocity = Vector3.zero;

        // Aplicar fuerza
        rb.AddForce(jumpDirection * wallJumpForce, ForceMode.Impulse);

        // Salir del wallrun
        isWallRunning = false;
        rb.useGravity = true;
    }


    private bool CanClimb()
    {
        if (isGrounded || isWallRunning) return false;
        return Physics.Raycast(transform.position, transform.forward, climbRayLength, wallLayer);
    }

    private void ClimbWall()
    {
        rb.useGravity = false;
        rb.linearVelocity = new Vector3(0, climbSpeed, 0);

        if (isGrounded || !Physics.Raycast(transform.position, transform.forward, climbRayLength, wallLayer))
        {
            isClimbing = false;
            rb.useGravity = true;
        }
    }

    public void Die()
    {
        inputEnabled = false;
        rb.linearVelocity = Vector3.zero;
        animator.SetTrigger("Morir");
    }
}
