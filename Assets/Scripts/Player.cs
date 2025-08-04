using UnityEngine.InputSystem;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Player : MonoBehaviour
{
    public Weapon weapon;

    [Header("Movimiento")]
    public float moveSpeed = 5f;
    private Vector2 moveInput;

    [Header("Rotación con mouse")]
    public Camera mainCamera;
    public LayerMask groundMask;

    [Header("Salto")]
    public float jumpForce = 5f;
    private bool isJumping = false;

    [Header("Agacharse")]
    public float crouchScale = 0.5f;
    private Vector3 originalScale;
    private bool isCrouching = false;

    [Header("Disparo automático")]
    private bool isAttacking = false;
    private float fireTimer = 0f;
    public float autoFireRate = 0.2f;
    private bool isAutoShooting = false;

    private Rigidbody rb;
    private Animator animator;

    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckDistance = 0.2f;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        originalScale = transform.localScale;
        animator = GetComponent<Animator>();
    }

    // Movimiento (WASD o joystick izquierdo)
    public void OnMove(InputAction.CallbackContext context)
    {
        // Si está disparando, no se puede mover
        if (isAttacking)
        {
            moveInput = Vector2.zero;
            return;
        }

        moveInput = context.ReadValue<Vector2>();
    }

    // Salto (barra espaciadora)
    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded() && !isCrouching)
        {
            isJumping = true;
        }
    }

    // Agacharse (CTRL)
    public void OnCrouch(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isCrouching = true;
            transform.localScale = new Vector3(originalScale.x, originalScale.y * crouchScale, originalScale.z);
            animator.SetBool("isCrouching", true);
        }
        else if (context.canceled)
        {
            isCrouching = false;
            transform.localScale = originalScale;
            animator.SetBool("isCrouching", false);
        }
    }

    public void Die()
    {
        animator.SetTrigger("Morir");
        this.enabled = false;
    }

    // Disparar (click izquierdo)
    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isAttacking = true;

            if (animator.GetBool("isMoving"))
            {
                animator.SetBool("isShooting", true);
                weapon.Shoot();
            }
        }
        else if (context.canceled)
        {
            isAttacking = false;
            animator.SetBool("isShooting", false);
            fireTimer = 0f;
            isAutoShooting = false;
        }
    }

    void Update()
    {
        // Rotación con el mouse
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundMask))
        {
            Vector3 lookPoint = hit.point;
            lookPoint.y = transform.position.y;

            Vector3 direction = lookPoint - transform.position;
            if (direction.magnitude > 0.1f)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 15f);
            }
        }

        bool isCurrentlyMoving = moveInput.magnitude > 0.1f;
        animator.SetBool("isMoving", isCurrentlyMoving);

        // Disparo automático si está quieto
        if (isAttacking && !isCurrentlyMoving)
        {
            isAutoShooting = true;
            fireTimer -= Time.deltaTime;

            if (fireTimer <= 0f)
            {
                weapon.Shoot();
                fireTimer = autoFireRate;
                animator.SetBool("isShooting", true);
            }
        }
        else
        {
            isAutoShooting = false;
        }
    }

    void FixedUpdate()
    {
        if (isJumping)
        {
            animator.SetTrigger("JumpTrig");
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isJumping = false;
        }

        // Movimiento
        Vector3 move = new Vector3(moveInput.x, 0, moveInput.y) * moveSpeed;
        move.y = rb.linearVelocity.y;
        rb.linearVelocity = move;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(groundCheck.position, Vector3.down, groundCheckDistance, groundMask);
    }
}