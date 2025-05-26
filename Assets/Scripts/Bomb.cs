using UnityEngine;

public class Bomb : MonoBehaviour
{
    public float fallSpeed = 1f;
    public AudioSource bombAudio; 

    void Update()
    {
        transform.position += Vector3.down * fallSpeed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (bombAudio != null)
            bombAudio.Play(); 

        if (other.CompareTag("Humvee"))
        {
            Destroy(other.gameObject); 
            Destroy(gameObject); 

           
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddEnemyPoint();
            }
        }
        else if (other.CompareTag("Enemy"))
        {
            Destroy(gameObject); 
        }
    }
}
