using UnityEngine;

public class MouseLookController : MonoBehaviour
{
    public Transform playerBody;
    public Camera mainCamera;
    public LayerMask groundLayer;
    [SerializeField] float rotationSpeed = 10f;

    void Update()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundLayer))
        {
            Vector3 lookDirection = hit.point - playerBody.position;
            lookDirection.y = 0f;

            if (lookDirection.sqrMagnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                playerBody.rotation = Quaternion.Slerp(playerBody.rotation, targetRotation, Time.deltaTime * rotationSpeed);
            }
        }
    }
}
