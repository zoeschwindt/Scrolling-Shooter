using UnityEngine;

public class MoveBoss : MonoBehaviour
{
    public float moveSpeed = 5f;
    private bool shouldMove = false;
    private float targetZ;

    public void StartMoving(float targetZPosition)
    {
        shouldMove = true;
        targetZ = targetZPosition;
    }

    void Update()
    {
        if (!shouldMove) return;

        Vector3 targetPos = new Vector3(transform.position.x, transform.position.y, targetZ);
        transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            shouldMove = false;
        }
    }
}
