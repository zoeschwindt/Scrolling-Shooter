using UnityEngine;

public class SimpleCameraCollision : MonoBehaviour
{
    [SerializeField] private Transform player;      
    [SerializeField] private float defaultDistance = 4f;
    [SerializeField] private float minDistance = 1f;
    [SerializeField] private float smoothSpeed = 10f;

    private Vector3 desiredCameraPos;
    private float currentDistance;

    void Start()
    {
        currentDistance = defaultDistance;
    }

    void LateUpdate()
    {
        Vector3 direction = (transform.position - player.position).normalized;
        Vector3 targetPosition = player.position + direction * defaultDistance;

        RaycastHit hit;
        if (Physics.Raycast(player.position, direction, out hit, defaultDistance))
        {
            currentDistance = Mathf.Clamp(hit.distance, minDistance, defaultDistance);
        }
        else
        {
            currentDistance = defaultDistance;
        }

        desiredCameraPos = player.position + direction * currentDistance;
        transform.position = Vector3.Lerp(transform.position, desiredCameraPos, Time.deltaTime * smoothSpeed);
        transform.LookAt(player);
    }
}
