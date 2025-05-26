using UnityEngine;
using UnityEngine.InputSystem;

public class Playermovement : MonoBehaviour
{
    public float moveSpeed = 10f;
    public float tiltAmountZ = 30f;
    public float tiltAmountX = 20f;
    public float tiltSpeed = 5f;

    Vector2 moveInput;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 move = new Vector3(moveInput.x, moveInput.y, 0f);
        Vector3 newPosition = rb.position + move * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPosition);

        float targetZRotation = -moveInput.x * tiltAmountZ;
        float targetXRotation = -moveInput.y * tiltAmountX;

        Quaternion targetRotation = Quaternion.Euler(targetXRotation, 0f, targetZRotation);
        rb.MoveRotation(Quaternion.Lerp(rb.rotation, targetRotation, Time.fixedDeltaTime * tiltSpeed));
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            GetComponent<PlayerHealth>().TakeDamage(100f);
        }
        else if (collision.gameObject.CompareTag("AvionEnemigo"))
        {
            GetComponent<PlayerHealth>().TakeDamage(100f);
        }
        else if (collision.gameObject.CompareTag("BalaEnemiga"))
        {
            GetComponent<PlayerHealth>().TakeDamage(10f);
        }
        else if (collision.gameObject.CompareTag("ProyectilSuave"))
        {
            GetComponent<PlayerHealth>().TakeDamage(5f);
        }
    }



}
