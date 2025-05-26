using UnityEngine;

public class Bombs : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Sumar bomba al jugador
            PlayerBombDropper bombDropper = other.GetComponent<PlayerBombDropper>();
            if (bombDropper != null)
            {
                bombDropper.AddBomb();
            }

            Destroy(gameObject);
        }
    }
}
