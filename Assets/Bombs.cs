using UnityEngine;

public class Bombs : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Esto usa el mismo ScoreManager que suma puntos cuando muere un enemigo
            if (ScoreManager.instance != null)
            {
                ScoreManager.instance.AddBombPoint();
            }

            Destroy(gameObject);
        }
    }
}
