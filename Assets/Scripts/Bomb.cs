using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float fallSpeed = 20f; // Aument� este valor para que caiga m�s r�pido

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }
}
