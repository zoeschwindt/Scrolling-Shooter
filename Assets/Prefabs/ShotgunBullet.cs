using UnityEngine;

public class ShotgunBullet : MonoBehaviour
{
    public float speed = 25f;
    private Vector3 direction;

    public void SetDirection(Vector3 targetPosition)
    {
        direction = (targetPosition - transform.position).normalized;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;
    }
}
