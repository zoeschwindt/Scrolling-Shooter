using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float fallSpeed = 20f; // Aumentá este valor para que caiga más rápido

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }
}
