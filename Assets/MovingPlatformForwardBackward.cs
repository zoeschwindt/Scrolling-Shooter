using UnityEngine;

public class MovingPlatformForwardBackward : MonoBehaviour
{
    [Header("Movimiento")]
    public float moveSpeed = 2f;
    public float distance = 5f;

    private Vector3 startPos;
    private Vector3 endPos;
    private bool goingForward = true;

    void Start()
    {
        startPos = transform.position;
        endPos = startPos + transform.forward * distance;
    }

    void Update()
    {
        Vector3 target = goingForward ? endPos : startPos;
        transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target) < 0.01f)
        {
            goingForward = !goingForward;
        }
    }
}
