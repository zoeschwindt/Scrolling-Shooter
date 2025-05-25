using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float fallSpeed = 20f; 

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }
}
